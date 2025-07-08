using CarBook.Dto.RentACarDtos;
using CarBook.WebUI.Services.RentACarServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace CarBook.WebUI.Controllers
{
    public class RentACarListController : Controller
    {
        private readonly IRentACarService _rentACarService;

        public RentACarListController(IRentACarService rentACarService)
        {
            _rentACarService = rentACarService;
        }

        public async Task<IActionResult> Index(FilterRentACarDto filterRentACarDto)
        {
            DateTime tempPickUpDate = filterRentACarDto.PickUpDate.Add(filterRentACarDto.PickUpTime.ToTimeSpan());
            TimeSpan manuelTimeZone = TimeSpan.FromHours(3);
            DateTimeOffset combinedPickUpDate = new DateTimeOffset(tempPickUpDate, manuelTimeZone);

            DateTime tempDropOffDate = filterRentACarDto.DropOffDate.Add(filterRentACarDto.DropOffTime.ToTimeSpan());
            DateTimeOffset combinedDropOffDate = new DateTimeOffset(tempDropOffDate, manuelTimeZone);

            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
            query["id"] = filterRentACarDto.PickUpLocationID.ToString();
            query["state"] = "true";
            query["pickUpDate"] = combinedPickUpDate.ToString("o");
            query["dropOffDate"] = combinedDropOffDate.ToString("o");

            List<ResultRentACarDto> values = await _rentACarService.GetRentACarWithAvailablity(query);

            query["dropOffLocationId"] = filterRentACarDto.DropOffLocationID.ToString();

            ViewBag.QueryString = query;

            filterRentACarDto.ResultDatas = values;

            return View(filterRentACarDto);
        }
    }
}
