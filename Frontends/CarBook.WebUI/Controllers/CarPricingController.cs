using CarBook.Dto.CarDtos;
using CarBook.Dto.CarPricingDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.CarServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarBook.WebUI.Controllers
{
    public class CarPricingController : Controller
    {
        private readonly ICarService _carService;

        public CarPricingController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            List<ResultCarDto> values = await _carService.GetCarForOnlyWithPricing();

            CarUIViewModel model = new CarUIViewModel();

            model.CarDatas = values;

            return View(model);
        }
    }
}
