using CarBook.Dto.StatisticsDtos;
using CarBook.WebUI.Services.StatisticsServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultStatisticsComponentPartial : ViewComponent
    {
        private readonly IStatisticsService _statisticsService;

        public _DefaultStatisticsComponentPartial(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int getCarCount = await _statisticsService.GetCarCountAsync();
            int getLocationCount = await _statisticsService.GetLocationCountAsync();
            int getBrandCount = await _statisticsService.GetBrandCountAsync();
            int getCarCountByFuelElectric = await _statisticsService.GetCarCountByFuelElectricAsync();

            ViewBag.CarCount = getCarCount;
            ViewBag.CarCountName = "Araç Sayısı";

            ViewBag.LocationCount = getLocationCount;
            ViewBag.LocationCountName = "Lokasyon Sayısı";

            ViewBag.BrandCount = getBrandCount;
            ViewBag.BrandCountName = "Marka Sayısı";

            ViewBag.CarCountByFuelElectric = getCarCountByFuelElectric;
            ViewBag.CarCountByFuelElectricName = "Elektrikli Araç Sayısı";

            return View();
        }
    }
}
