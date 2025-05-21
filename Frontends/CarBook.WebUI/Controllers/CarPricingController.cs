using CarBook.Dto.CarDtos;
using CarBook.Dto.CarPricingDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarBook.WebUI.Controllers
{
    public class CarPricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public CarPricingController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/cars/getcarforonlywithpricing");

            CarUIViewModel model = new CarUIViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonData = await responseMessage.Content.ReadAsStringAsync();
                List<ResultCarDto> values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);

                model.CarDatas = values;
            }

            return View(model);
        }
    }
}
