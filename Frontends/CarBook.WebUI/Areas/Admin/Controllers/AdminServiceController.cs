using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Service")]
    public class AdminServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminServiceController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/services");

            AdminUIServiceViewModel model = new AdminUIServiceViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateService(AdminUIServiceViewModel adminUIServiceViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateServiceDto>($"{_apiSettings.ApiBaseUrl}/services", adminUIServiceViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminService", new { area = "Admin" });
            }

            return View(adminUIServiceViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/services/{id}");

            AdminUIServiceViewModel model = new AdminUIServiceViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateServiceDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateService(AdminUIServiceViewModel adminUIServiceViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateServiceDto>($"{_apiSettings.ApiBaseUrl}/services", adminUIServiceViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminService", new { area = "Admin" });
            }

            return View(adminUIServiceViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/services?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminService", new { area = "Admin" });
        }
    }
}
