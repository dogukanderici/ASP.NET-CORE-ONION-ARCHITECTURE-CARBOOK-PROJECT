using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.BannerServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Banner")]
    public class AdminBannerController : AdminBaseController
    {
        private readonly IBannerService _bannerService;

        public AdminBannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultBannerDto> serviceResponse = await _bannerService.GetBannerAsync();

            AdminUIBannerViewModel model = new AdminUIBannerViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateBanner()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBanner(AdminUIBannerViewModel adminUIBannerViewModel)
        {
            HttpResponseMessage serviceResponse = await _bannerService.CreateBannerAsync(adminUIBannerViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.Content;
            }

            return View(adminUIBannerViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            UIServiceApiResponseSetting<ResultBannerDto> serviceResponse = await _bannerService.GetBannerByIdAsync(id);

            AdminUIBannerViewModel model = new AdminUIBannerViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateBannerDto value = JsonConvert.DeserializeObject<UpdateBannerDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBanner(AdminUIBannerViewModel adminUIBannerViewModel)
        {
            HttpResponseMessage serviceResponse = await _bannerService.UpdateBannerAsync(adminUIBannerViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.Content;
            }

            return View(adminUIBannerViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            HttpResponseMessage serviceResponse = await _bannerService.DeleteBannerAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.UIErrorCode = serviceResponse.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.Content;
            }

            return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
        }
    }
}
