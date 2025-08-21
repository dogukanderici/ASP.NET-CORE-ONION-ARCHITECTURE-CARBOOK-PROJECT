using CarBook.Dto.CarDtos;
using CarBook.Dto.CarFeatureDtos;
using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.CarFeatureServices;
using CarBook.WebUI.Services.CarServices;
using CarBook.WebUI.Services.FeatureServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarFeature")]
    public class AdminCarFeatureController : AdminBaseController
    {
        private readonly ICarFeatureService _carFeatureService;
        private readonly ICarService _carService;
        private readonly IFeatureService _featureService;

        public AdminCarFeatureController(ICarFeatureService carFeatureService, ICarService carService, IFeatureService featureService)
        {
            _carFeatureService = carFeatureService;
            _carService = carService;
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            UIServiceApiResponseSetting<ResultCarFeatureDto> serviceResponse = await _carFeatureService.GetCarFeatureByIdAsync(id);

            AdminUICarFeatureViewModel model = new AdminUICarFeatureViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarFeatureData = serviceResponse.ResponseData;
            }

            return View(model);
        }

        [HttpGet("CarFeatureDetail")]
        public async Task<IActionResult> CarFeatureDetail(int id)
        {
            UIServiceApiResponseSetting<ResultCarDto> serviceResponse = await _carService.GetCarByIdAsync(id);

            List<SelectListItem> featureList = await GetFeatures();

            ViewBag.FeatureList = featureList;
            ViewBag.CarID = id;

            AdminUICarFeatureViewModel model = new AdminUICarFeatureViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarFeatureForCarDatas = serviceResponse.ResponseData.CarFeatures;
            }

            return View(model);

        }

        [HttpPost("CarFeatureDetail")]
        public async Task<IActionResult> CarFeatureDetail(AdminUICarFeatureViewModel adminUICarFeatureViewModel)
        {
            HttpResponseMessage serviceResponse = await _carFeatureService.CreateCarFeatureAsync(adminUICarFeatureViewModel.CreateCarFeatureDatas);

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("CarFeatureDetail", "AdminCarFeature", new { area = "Admin", id = adminUICarFeatureViewModel.CreateCarFeatureDatas[0].CarID });
            }

            return View(adminUICarFeatureViewModel);
        }

        [HttpGet("Remove")]
        public async Task<IActionResult> RemoveCarFeature(int id)
        {
            HttpResponseMessage serviceResponse = await _carFeatureService.DeleteCarFeatureAsync(id);

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });

        }

        private async Task<List<SelectListItem>> GetFeatures()
        {
            UIServiceApiResponseSetting<ResultFeatureDto> serviceResponse = await _featureService.GetFeatureAsync();

            List<SelectListItem> featureList = new List<SelectListItem>();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                List<ResultFeatureDto> values = serviceResponse.ResponseDatas;

                featureList = (from item in values
                               select new SelectListItem
                               {
                                   Text = item.FeatureName,
                                   Value = item.FeatureID.ToString()
                               }).ToList();
            }

            return featureList;
        }
    }
}
