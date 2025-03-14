using CarBook.Dto.BrandDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class AdminBrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminBrandController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/brands");

            AdminUIBrandViewModel model = new AdminUIBrandViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

                model.ResultDatas = values;
            }

            return View(model);
        }


        [HttpGet("Create")]
        public IActionResult CreateBrand()
        {
            AdminUIBrandViewModel model = new AdminUIBrandViewModel();

            return View(model);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateBrand(AdminUIBrandViewModel adminUIBrandViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateBrandDto>($"{_apiSettings.ApiBaseUrl}/brands", adminUIBrandViewModel.CreateData);

            var apiMessage2 = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBrand", new { area = "Admin" });
            }

            return View(adminUIBrandViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/brands/{id}");

            AdminUIBrandViewModel model = new AdminUIBrandViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBrandDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBrand(AdminUIBrandViewModel adminUIBrandViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateBrandDto>($"{_apiSettings.ApiBaseUrl}/brands", adminUIBrandViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiResponse = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBrand", new { area = "Admin" });
            }

            return View(adminUIBrandViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/brands?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiResponse = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminBrand", new { area = "Admin" });
        }
    }
}
