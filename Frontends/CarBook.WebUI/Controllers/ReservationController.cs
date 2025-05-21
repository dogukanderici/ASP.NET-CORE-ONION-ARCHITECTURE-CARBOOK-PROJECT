using CarBook.Dto.LocationDtos;
using CarBook.Dto.ReservationDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace CarBook.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public ReservationController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var queryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());

            string pickUpLocationId = queryString["id"];
            string dropOffLocationId = queryString["dropOffLocationId"];
            string state = queryString["state"];
            string pickUpDate = queryString["pickUpDate"];
            string dropOffDate = queryString["dropOffDate"];
            string carId = queryString["carID"];
            string carModel = queryString["carModel"];
            string carBrand = queryString["carBrand"];

            TimeSpan manuelTimeZone = TimeSpan.FromHours(3);

            ReservationUIViewModel model = new ReservationUIViewModel();

            model.CreateData.CarID = Convert.ToInt32(carId);
            model.CreateData.PickUpLocationID = Convert.ToInt32(pickUpLocationId);
            model.CreateData.DropOffLocationID = Convert.ToInt32(dropOffLocationId);
            model.CreateData.PickUpDate = Convert.ToDateTime(pickUpDate);
            model.CreateData.DropOffDate = Convert.ToDateTime(dropOffDate);

            ViewBag.PageRouteTitle = "Rezervasyon Bilgileri";
            ViewBag.CarModel = carModel;
            ViewBag.CarBrand = carBrand;
            ViewBag.LocationList = await GetLocationListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ReservationUIViewModel reservationUIViewModel)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.PostAsJsonAsync<CreateReservationDto>($"{_apiSettings.ApiBaseUrl}/reservations", reservationUIViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }

            var stringData = await responseMessage.Content.ReadAsStringAsync();

            ViewBag.HttpStatusCode = responseMessage.StatusCode;
            ViewBag.ResponseMessage = stringData;

            return View(reservationUIViewModel);
        }

        private async Task<List<SelectListItem>> GetLocationListAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/locations");

            List<SelectListItem> locationList = new List<SelectListItem>();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                locationList = (from item in value
                                select new SelectListItem
                                {
                                    Text = item.LocationName,
                                    Value = item.LocationID.ToString()
                                }).ToList();
            }

            return locationList;
        }
    }
}
