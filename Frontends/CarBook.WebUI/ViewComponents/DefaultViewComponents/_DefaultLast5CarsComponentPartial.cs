using CarBook.Dto.CarDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.CarServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultLast5CarsComponentPartial : ViewComponent
    {
        private readonly ICarService _carService;

        public _DefaultLast5CarsComponentPartial(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultCarDto> values = await _carService.GetLast5CarsAsync();

            CarUIViewModel model = new CarUIViewModel();

            model.CarDatas = values;

            return View(model);
        }
    }
}
