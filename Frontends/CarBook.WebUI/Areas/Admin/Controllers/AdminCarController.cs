using CarBook.Dto.BrandDtos;
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
using System.Text;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Car")]
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminCarController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/cars");

            AdminUICarViewModel model = new AdminUICarViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);

                model.CarDatas = values;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateCar()
        {
            List<SelectListItem> brandList = await CarBrands();
            List<SelectListItem> transmissionList = GetTransmissionType();
            List<SelectListItem> fuelTypeList = GetFuelType();

            ViewBag.BrandList = brandList;
            ViewBag.TransmissionList = transmissionList;
            ViewBag.FuelTypeList = fuelTypeList;

            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCar(AdminUICarViewModel adminUICarViewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(adminUICarViewModel.CreateCarData);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsJsonAsync<CreateCarDto>($"{_apiSettings.ApiBaseUrl}/cars", adminUICarViewModel.CreateCarData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }

            return View(adminUICarViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateCar(int id)
        {
            List<SelectListItem> brandList = await CarBrands();
            List<SelectListItem> transmissionList = GetTransmissionType();
            List<SelectListItem> fuelTypeList = GetFuelType();

            ViewBag.BrandList = brandList;
            ViewBag.TransmissionList = transmissionList;
            ViewBag.FuelTypeList = fuelTypeList;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/cars/{id}");

            AdminUICarViewModel model = new AdminUICarViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCarDto>(jsonData);

                model.UpdateCarData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCar(AdminUICarViewModel adminUICarViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateCarDto>($"{_apiSettings.ApiBaseUrl}/cars", adminUICarViewModel.UpdateCarData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }

            return View(adminUICarViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/cars?id={id}");

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
        }

        private async Task<List<SelectListItem>> CarBrands()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/brands");

            List<SelectListItem> brandList = new List<SelectListItem>();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

                brandList = (from item in values
                             select new SelectListItem
                             {
                                 Text = item.BrandName,
                                 Value = item.BrandID.ToString()
                             }).ToList();
            }

            return brandList;
        }

        private List<SelectListItem> GetFuelType()
        {
            List<SelectListItem> fuelTypeList = new List<SelectListItem>();


            fuelTypeList.Add(new SelectListItem
            {
                Text = "Benzin",
                Value = "Benzin"
            });

            fuelTypeList.Add(new SelectListItem
            {
                Text = "Dizel",
                Value = "Dizel"
            });

            fuelTypeList.Add(new SelectListItem
            {
                Text = "Hibrit",
                Value = "Hibrit"
            });

            fuelTypeList.Add(new SelectListItem
            {
                Text = "Elektrik",
                Value = "Elektrik"
            });

            return fuelTypeList;
        }

        private List<SelectListItem> GetTransmissionType()
        {
            List<SelectListItem> transmissionList = new List<SelectListItem>();

            transmissionList.Add(new SelectListItem
            {
                Text = "Manuel",
                Value = "Manuel"
            });

            transmissionList.Add(new SelectListItem
            {
                Text = "Otomatik",
                Value = "Otomatik"
            });

            return transmissionList;
        }
    }
}
