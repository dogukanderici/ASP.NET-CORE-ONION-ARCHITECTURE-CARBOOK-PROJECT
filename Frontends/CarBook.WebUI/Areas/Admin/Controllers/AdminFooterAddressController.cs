using CarBook.Dto.FooterAddressDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.FooterAddressServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/FooterAddress")]
    public class AdminFooterAddressController : AdminBaseController
    {
        private readonly IFooterAddressService _footerAddressService;

        public AdminFooterAddressController(IFooterAddressService footerAddressService)
        {
            _footerAddressService = footerAddressService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultFooterAddressDto> serviceResponse = await _footerAddressService.GetFooterAddressAsync();

            AdminUIFooterAddressViewModel model = new AdminUIFooterAddressViewModel();

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
        public IActionResult CreateFooterAddress()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateFooterAddress(AdminUIFooterAddressViewModel adminUIFooterAddressViewModel)
        {
            HttpResponseMessage serviceResponse = await _footerAddressService.CreateFooterAddressAsync(adminUIFooterAddressViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFooterAddress", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUIFooterAddressViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateFooterAddress(int id)
        {
            UIServiceApiResponseSetting<ResultFooterAddressDto> serviceResponse = await _footerAddressService.GetFooterAddressByIdAsync(id);

            AdminUIFooterAddressViewModel model = new AdminUIFooterAddressViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateFooterAddressDto value = JsonConvert.DeserializeObject<UpdateFooterAddressDto>(jsonData);

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
        public async Task<IActionResult> UpdateFooterAddress(AdminUIFooterAddressViewModel adminUIFooterAddressViewModel)
        {
            HttpResponseMessage serviceResponse = await _footerAddressService.UpdateFooterAddressAsync(adminUIFooterAddressViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFooterAddress", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUIFooterAddressViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteFooterAddress(int id)
        {
            HttpResponseMessage serviceResponse = await _footerAddressService.DeleteFooterAddressAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminFooterAddress", new { area = "Admin" });
        }
    }
}
