using CarBook.Dto.CarDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.CarServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageRouteTitle = "Araçlarımız";

            UIServiceApiResponseSetting<ResultCarDto> serviceResponse = await _carService.GetCarsAsync();

            CarUIViewModel model = new CarUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarDatas = serviceResponse.ResponseDatas;
            }

            return View(model);
        }


        public async Task<IActionResult> CarDetail(int id)
        {
            ViewBag.PageRouteTitle = "Araç Detayı";

            UIServiceApiResponseSetting<ResultCarDto> serviceResponse = await _carService.GetCarByIdAsync(id);

            CarUIViewModel model = new CarUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarData = serviceResponse.ResponseData;
            }

            return View(model);
        }
    }
}
