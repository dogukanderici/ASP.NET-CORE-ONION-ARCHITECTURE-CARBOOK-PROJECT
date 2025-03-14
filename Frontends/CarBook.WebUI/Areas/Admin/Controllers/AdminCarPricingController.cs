using CarBook.Dto.CarPricingDtos;
using CarBook.Dto.PricingTypeDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarPricing")]
    public class AdminCarPricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminCarPricingController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/carpricings/getcarpricingbycarid?id={id}");

            AdminUICarPricingViewModel model = new AdminUICarPricingViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultCarPricingForCarDto>>(jsonData);

                model.ResultForCarDatas = value;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateCarPricing(int id)
        {
            AdminUICarPricingViewModel model = new AdminUICarPricingViewModel();
            model.CreateData = new CreateCarPricingDto();

            model.CreateData.CarID = id;

            ViewBag.CarId = id;
            ViewBag.PricingTypeList = await GetPricingTypeAsync();

            return View(model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCarPricing(AdminUICarPricingViewModel adminUICarPricingViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateCarPricingDto>($"{_apiSettings.ApiBaseUrl}/carpricings", adminUICarPricingViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminCarPricing", new { area = "Admin", id = adminUICarPricingViewModel.CreateData.CarID });
            }

            return View(adminUICarPricingViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateCarPricing(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/carpricings/{id}");

            AdminUICarPricingViewModel model = new AdminUICarPricingViewModel();

            ViewBag.PricingTypeList = await GetPricingTypeAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCarPricingDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCarPricing(AdminUICarPricingViewModel adminUICarPricingViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateCarPricingDto>($"{_apiSettings.ApiBaseUrl}/carpricings", adminUICarPricingViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminCarPricing", new { area = "Admin", id = adminUICarPricingViewModel.UpdateData.CarID });
            }

            return View(adminUICarPricingViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteCarPricing(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/carpricings?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
        }

        private async Task<List<SelectListItem>> GetPricingTypeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/pricingtypes");

            List<SelectListItem> dataList = new List<SelectListItem>();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultPricingTypeDto>>(jsonData);

                dataList = (from item in value
                            select new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.PricingTypeID.ToString()
                            }).ToList();
            }

            return dataList;
        }
    }
}
