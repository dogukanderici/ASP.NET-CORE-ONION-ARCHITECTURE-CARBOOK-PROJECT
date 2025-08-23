using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.SocialMediaServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SocialMedia")]
    public class AdminSocialMediaController : AdminBaseController
    {
        private readonly ISocialMediaService _socialMediaService;

        public AdminSocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultSocialMediaDto> serviceResponse = await _socialMediaService.GetSocialMediaAsync();

            AdminUISocialMediaViewModel model = new AdminUISocialMediaViewModel();

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
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSocialMedia(AdminUISocialMediaViewModel adminUISocialMediaViewModel)
        {
            HttpResponseMessage serviceResponse = await _socialMediaService.CreateSocialMediaAsync(adminUISocialMediaViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUISocialMediaViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            UIServiceApiResponseSetting<ResultSocialMediaDto> serviceResponse = await _socialMediaService.GetSocialMediaByIdAsync(id);

            AdminUISocialMediaViewModel model = new AdminUISocialMediaViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateSocialMediaDto value = JsonConvert.DeserializeObject<UpdateSocialMediaDto>(jsonData);

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
        public async Task<IActionResult> UpdateSocialMedia(AdminUISocialMediaViewModel adminUISocialMediaViewModel)
        {
            HttpResponseMessage serviceResponse = await _socialMediaService.UpdateSocialMediaAsync(adminUISocialMediaViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUISocialMediaViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            HttpResponseMessage serviceResponse = await _socialMediaService.DeleteSocialMediaAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
        }
    }
}
