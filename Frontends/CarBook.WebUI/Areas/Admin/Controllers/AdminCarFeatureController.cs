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
    public class AdminCarFeatureController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarFeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync($"carfeatures/{id}");

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
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync($"cars/{id}");

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
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<List<CreateCarFeatureDto>>("carfeatures/createcarfeaturewithlist", adminUICarFeatureViewModel.CreateCarFeatureDatas);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CarFeatureDetail", "AdminCarFeature", new { area = "Admin", id = adminUICarFeatureViewModel.CreateCarFeatureDatas[0].CarID });
            }

            return View(adminUICarFeatureViewModel);
        }

        private async Task<List<SelectListItem>> GetFeatures()
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("features");

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
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.DeleteAsync($"carfeatures?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });

        }
    }
}
