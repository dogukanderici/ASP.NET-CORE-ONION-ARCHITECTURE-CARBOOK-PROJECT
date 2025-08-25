using CarBook.Dto.CarPricingDtos;
using CarBook.Dto.PricingTypeDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.CarPricingServices;
using CarBook.WebUI.Services.PricingTypeServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarPricing")]
    public class AdminCarPricingController : AdminBaseController
    {
        private readonly ICarPricingService _carPricingService;
        private readonly IPricingTypeService _pricingTypeService;

        public AdminCarPricingController(ICarPricingService carPricingService, IPricingTypeService pricingTypeService)
        {
            _carPricingService = carPricingService;
            _pricingTypeService = pricingTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.CarId = id;

            UIServiceApiResponseSetting<ResultCarPricingForCarDto> serviceResponse = await _carPricingService.GetCarPricingAsync(id);

            AdminUICarPricingViewModel model = new AdminUICarPricingViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultForCarDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateCarPricing(int id)
        {
            AdminUICarPricingViewModel model = new AdminUICarPricingViewModel();
            model.CreateData = new CreateCarPricingDto();

            model.CreateData.CarID = id;

            ViewBag.CarId = id;
            ViewBag.PricingTypeList = await GetPricingTypeAsync();

            return View(model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCarPricing(AdminUICarPricingViewModel adminUICarPricingViewModel)
        {
            HttpResponseMessage serviceResponse = await _carPricingService.CreateCarPricingAsync(adminUICarPricingViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCarPricing", new { area = "Admin", id = adminUICarPricingViewModel.CreateData.CarID });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUICarPricingViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateCarPricing(int id)
        {
            UIServiceApiResponseSetting<ResultCarPricingDto> serviceResponse = await _carPricingService.GetCarPricingByIdAsync(id);

            AdminUICarPricingViewModel model = new AdminUICarPricingViewModel();

            UpdateCarPricingDto value = new UpdateCarPricingDto();

            ViewBag.PricingTypeList = await GetPricingTypeAsync();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<UpdateCarPricingDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCarPricing(AdminUICarPricingViewModel adminUICarPricingViewModel)
        {
            HttpResponseMessage serviceResponse = await _carPricingService.UpdateCarPricingAsync(adminUICarPricingViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCarPricing", new { area = "Admin", id = adminUICarPricingViewModel.UpdateData.CarID });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUICarPricingViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteCarPricing(int id)
        {
            HttpResponseMessage serviceResponse = await _carPricingService.DeleteCarPricingAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
        }

        private async Task<List<SelectListItem>> GetPricingTypeAsync()
        {
            UIServiceApiResponseSetting<ResultPricingTypeDto> serviceResponse = await _pricingTypeService.GetPricingTypeAsync();

            List<SelectListItem> dataList = new List<SelectListItem>();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                List<ResultPricingTypeDto> values = serviceResponse.ResponseDatas;

                dataList = (from item in values
                            select new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.PricingTypeID.ToString()
                            }).ToList();
            }

            return dataList;
        }
    }
}
