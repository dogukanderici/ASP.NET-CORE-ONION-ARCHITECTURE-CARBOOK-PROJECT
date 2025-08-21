using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.FeatureServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class AdmincarFeatureDescriptionController : AdminBaseController
    {
        private readonly IFeatureService _featureService;

        public AdmincarFeatureDescriptionController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultFeatureDto> serviceResponse = await _featureService.GetFeatureAsync();

            AdminUIFeatureViewModel model = new AdminUIFeatureViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.FeatureDatas = serviceResponse.ResponseDatas;
            }

            return View(model);
        }


        [HttpGet("Create")]
        public IActionResult CreateFeature()
        {
            AdminUIFeatureViewModel model = new AdminUIFeatureViewModel();

            return View(model);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateFeature(AdminUIFeatureViewModel adminUIFeatureViewModel)
        {
            HttpResponseMessage serviceResponse = await _featureService.CreateFeatureAsync(adminUIFeatureViewModel.CreateDatas);

            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdmincarFeatureDescription", new { area = "Admin" });
            }

            return View(adminUIFeatureViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            UIServiceApiResponseSetting<ResultFeatureDto> serviceResponse = await _featureService.GetFeatureByIdAsync(id);

            AdminUIFeatureViewModel model = new AdminUIFeatureViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateFeatureDto value = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);

                model.UpdateDatas = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateFeature(AdminUIFeatureViewModel adminUIFeatureViewModel)
        {
            HttpResponseMessage serviceResponse = await _featureService.UpdateFeatureAsync(adminUIFeatureViewModel.UpdateDatas);
            string apiResponse = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdmincarFeatureDescription", new { area = "Admin" });
            }

            return View(adminUIFeatureViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            HttpResponseMessage serviceResponse = await _featureService.DeleteFeatureAsync(id);
            string apiResponse = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiResponse;
            }

            return RedirectToAction("Index", "AdmincarFeatureDescription", new { area = "Admin" });
        }
    }
}
