using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Banner")]
    public class AdminBannerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminBannerController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/banners");

            AdminUIBannerViewModel model = new AdminUIBannerViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultBannerDto>>(jsonData);

                model.ResultDatas = value;
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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateBannerDto>($"{_apiSettings.ApiBaseUrl}/banners", adminUIBannerViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
            }

            return View(adminUIBannerViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/banners/{id}");

            AdminUIBannerViewModel model = new AdminUIBannerViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBannerDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBanner(AdminUIBannerViewModel adminUIBannerViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateBannerDto>($"{_apiSettings.ApiBaseUrl}/banners", adminUIBannerViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
            }

            return View(adminUIBannerViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/banners?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
        }
    }
}
