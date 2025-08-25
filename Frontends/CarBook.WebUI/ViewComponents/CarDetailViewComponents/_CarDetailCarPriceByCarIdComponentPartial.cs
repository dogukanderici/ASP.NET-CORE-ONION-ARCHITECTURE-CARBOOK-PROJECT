using CarBook.Dto.CarPricingDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.CarPricingServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCarPriceByCarIdComponentPartial : ViewComponent
    {
        private readonly ICarPricingService _carPricingService;

        public _CarDetailCarPriceByCarIdComponentPartial(ICarPricingService carPricingService)
        {
            _carPricingService = carPricingService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int carId)
        {
            UIServiceApiResponseSetting<ResultCarPricingForCarDto> serviceResponse = await _carPricingService.GetCarPricingAsync(carId);

            CarPricingUIViewModel model = new CarPricingUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarPricingForCarDatas = serviceResponse.ResponseDatas;
            }

            return View(model);
        }
    }
}
