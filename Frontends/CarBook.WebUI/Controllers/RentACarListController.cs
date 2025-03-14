using CarBook.Dto.RentACarDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace CarBook.WebUI.Controllers
{
    public class RentACarListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public RentACarListController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index(FilterRentACarDto filterRentACarDto)
        {
            DateTime tempPickUpDate = filterRentACarDto.PickUpDate.Add(filterRentACarDto.PickUpTime.ToTimeSpan());
            TimeSpan manuelTimeZone = TimeSpan.FromHours(3);
            DateTimeOffset combinedPickUpDate = new DateTimeOffset(tempPickUpDate, manuelTimeZone);

            DateTime tempDropOffDate = filterRentACarDto.DropOffDate.Add(filterRentACarDto.DropOffTime.ToTimeSpan());
            DateTimeOffset combinedDropOffDate = new DateTimeOffset(tempDropOffDate, manuelTimeZone);

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["id"] = filterRentACarDto.PickUpLocationID.ToString();
            query["state"] = "true";
            query["pickUpDate"] = combinedPickUpDate.ToString("o");
            query["dropOffDate"] = combinedDropOffDate.ToString("o");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/RentACars/GetRentACarWithAvailablity?{query}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultRentACarDto>>(jsonData);

                filterRentACarDto.ResultDatas = value;
            }

            return View(filterRentACarDto);
        }
    }
}
