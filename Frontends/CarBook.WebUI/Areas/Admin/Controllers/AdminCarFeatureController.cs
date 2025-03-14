using CarBook.Dto.CarDtos;
using CarBook.Dto.CarFeatureDtos;
using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarFeature")]
    public class AdminCarFeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminCarFeatureController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/carfeatures/{id}");

            AdminUICarFeatureViewModel model = new AdminUICarFeatureViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultCarFeatureDto>(jsonData);

                model.CarFeatureData = value;
            }

            return View(model);
        }

        [HttpGet("CarFeatureDetail")]
        public async Task<IActionResult> CarFeatureDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/cars/{id}");

            List<SelectListItem> featureList = await GetFeatures();

            ViewBag.FeatureList = featureList;
            ViewBag.CarID = id;

            AdminUICarFeatureViewModel model = new AdminUICarFeatureViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultCarDto>(jsonData);

                model.CarFeatureForCarDatas = value.CarFeatures;
            }

            return View(model);

        }

        [HttpPost("CarFeatureDetail")]
        public async Task<IActionResult> CarFeatureDetail(AdminUICarFeatureViewModel adminUICarFeatureViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<List<CreateCarFeatureDto>>($"{_apiSettings.ApiBaseUrl}/carfeatures/createcarfeaturewithlist", adminUICarFeatureViewModel.CreateCarFeatureDatas);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CarFeatureDetail", "AdminCarFeature", new { area = "Admin", id = adminUICarFeatureViewModel.CreateCarFeatureDatas[0].CarID });
            }

            return View(adminUICarFeatureViewModel);
        }

        private async Task<List<SelectListItem>> GetFeatures()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/features");

            List<SelectListItem> featureList = new List<SelectListItem>();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);

                featureList = (from item in values
                               select new SelectListItem
                               {
                                   Text = item.FeatureName,
                                   Value = item.FeatureID.ToString()
                               }).ToList();
            }

            return featureList;
        }

        [HttpGet("Remove")]
        public async Task<IActionResult> RemoveCarFeature(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/carfeatures?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });

        }
    }
}
