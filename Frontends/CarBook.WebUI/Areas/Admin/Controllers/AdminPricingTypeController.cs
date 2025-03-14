using CarBook.Dto.PricingTypeDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/PricingType")]
    public class AdminPricingTypeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminPricingTypeController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/pricingtypes");

            AdminUIPricingTypeViewModel model = new AdminUIPricingTypeViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultPricingTypeDto>>(jsonData);

                model.ResultDatas = value;
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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreatePricingTypeDto>($"{_apiSettings.ApiBaseUrl}/pricingtypes", adminUIPricingTypeViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminPricingType", new { area = "Admin" });
            }

            return View(adminUIPricingTypeViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdatePricingType(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/pricingtypes/{id}");

            AdminUIPricingTypeViewModel model = new AdminUIPricingTypeViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdatePricingTypeDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdatePricingType(AdminUIPricingTypeViewModel adminUIPricingTypeViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdatePricingTypeDto>($"{_apiSettings.ApiBaseUrl}/pricingtypes", adminUIPricingTypeViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminPricingType", new { area = "Admin" });
            }

            return View(adminUIPricingTypeViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeletePricingType(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/pricingtypes?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminPricingType", new { area = "Admin" });
        }
    }
}
