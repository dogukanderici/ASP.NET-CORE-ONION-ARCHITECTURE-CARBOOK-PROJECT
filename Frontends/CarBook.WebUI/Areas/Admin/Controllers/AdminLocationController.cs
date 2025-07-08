using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Location")]
    public class AdminLocationController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminLocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("locations");

            AdminUILocationViewModel model = new AdminUILocationViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateLocation(AdminUILocationViewModel adminUILocationViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<CreateLocationDto>("locations", adminUILocationViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
            }

            return View(adminUILocationViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateLocation(int id)
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync($"locations/{id}");

            AdminUILocationViewModel model = new AdminUILocationViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateLocationDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateLocation(AdminUILocationViewModel adminUILocationViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PutAsJsonAsync<UpdateLocationDto>("locations", adminUILocationViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
            }

            return View(adminUILocationViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.DeleteAsync($"locations?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
        }
    }
}
