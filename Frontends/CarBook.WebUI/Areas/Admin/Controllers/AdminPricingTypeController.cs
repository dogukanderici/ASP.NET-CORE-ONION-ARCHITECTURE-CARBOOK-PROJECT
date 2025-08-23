using CarBook.Dto.PricingTypeDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.PricingTypeServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/PricingType")]
    public class AdminPricingTypeController : AdminBaseController
    {
        private readonly IPricingTypeService _pricingTypeService;

        public AdminPricingTypeController(IPricingTypeService pricingTypeService)
        {
            _pricingTypeService = pricingTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultPricingTypeDto> serviceResponse = await _pricingTypeService.GetPricingTypeAsync();

            AdminUIPricingTypeViewModel model = new AdminUIPricingTypeViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreatePricingType()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePricingType(AdminUIPricingTypeViewModel adminUIPricingTypeViewModel)
        {
            HttpResponseMessage serviceResponse = await _pricingTypeService.CreatePricingTypeAsync(adminUIPricingTypeViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminPricingType", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUIPricingTypeViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdatePricingType(int id)
        {
            UIServiceApiResponseSetting<ResultPricingTypeDto> serviceResponse = await _pricingTypeService.GetPricingTypeByIdAsync(id);

            AdminUIPricingTypeViewModel model = new AdminUIPricingTypeViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdatePricingTypeDto>(jsonData);

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
        public async Task<IActionResult> UpdatePricingType(AdminUIPricingTypeViewModel adminUIPricingTypeViewModel)
        {
            HttpResponseMessage serviceResponse = await _pricingTypeService.UpdatePricingTypeAsync(adminUIPricingTypeViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminPricingType", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUIPricingTypeViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeletePricingType(int id)
        {
            HttpResponseMessage serviceResponse = await _pricingTypeService.DeletePricingTypeAsync(id);

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.Content.ReadAsStringAsync(); ;
            }

            return RedirectToAction("Index", "AdminPricingType", new { area = "Admin" });
        }
    }
}
