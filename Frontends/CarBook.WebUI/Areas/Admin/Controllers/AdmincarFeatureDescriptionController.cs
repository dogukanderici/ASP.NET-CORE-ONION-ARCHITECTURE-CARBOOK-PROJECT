using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Areas.Admin.Models;
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
    public class AdmincarFeatureDescriptionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdmincarFeatureDescriptionController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/features");

            AdminUIFeatureViewModel model = new AdminUIFeatureViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);

                model.FeatureDatas = values;
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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateFeatureDto>($"{_apiSettings.ApiBaseUrl}/features", adminUIFeatureViewModel.CreateDatas);

            var apiMessage2 = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdmincarFeatureDescription", new { area = "Admin" });
            }

            return View(adminUIFeatureViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/features/{id}");

            AdminUIFeatureViewModel model = new AdminUIFeatureViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);

                model.UpdateDatas = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateFeature(AdminUIFeatureViewModel adminUIFeatureViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateFeatureDto>($"{_apiSettings.ApiBaseUrl}/features", adminUIFeatureViewModel.UpdateDatas);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiResponse = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdmincarFeatureDescription", new { area = "Admin" });
            }

            return View(adminUIFeatureViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/features?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiResponse = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdmincarFeatureDescription", new { area = "Admin" });
        }
    }
}
