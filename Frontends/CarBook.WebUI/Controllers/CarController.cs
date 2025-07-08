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

            List<ResultCarDto> values = await _carService.GetCarsAsync();

            CarUIViewModel model = new CarUIViewModel();

            model.CarDatas = values;

            return View(model);
        }


        public async Task<IActionResult> CarDetail(int id)
        {
            ViewBag.PageRouteTitle = "Araç Detayı";

            ResultCarDto value = await _carService.GetCarByIdAsync(id);

            CarUIViewModel model = new CarUIViewModel();

            model.CarData = value;

            return View(model);
        }
    }
}
