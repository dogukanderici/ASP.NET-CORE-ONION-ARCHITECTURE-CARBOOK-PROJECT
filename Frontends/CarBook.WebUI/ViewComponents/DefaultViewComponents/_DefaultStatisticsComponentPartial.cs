using CarBook.Dto.StatisticsDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _DefaultStatisticsComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var getCarCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarCount");
            if (getCarCount.IsSuccessStatusCode)
            {
                var jsonData = await getCarCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCount = value.CarCount;
                ViewBag.CarCountName = "Araç Sayısı";
            }

            var getLocationCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetLocationCount");
            if (getLocationCount.IsSuccessStatusCode)
            {
                var jsonData = await getLocationCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.LocationCount = value.LocationCount;
                ViewBag.LocationCountName = "Lokasyon Sayısı";
            }

            var getBrandCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetBrandCount");
            if (getBrandCount.IsSuccessStatusCode)
            {
                var jsonData = await getBrandCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.BrandCount = value.BrandCount;
                ViewBag.BrandCountName = "Marka Sayısı";
            }

            var getCarCountByFuelElectric = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarCountByFuelElectric");
            if (getCarCountByFuelElectric.IsSuccessStatusCode)
            {
                var jsonData = await getCarCountByFuelElectric.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCountByFuelElectric = value.CarCountByFuelElectric;
                ViewBag.CarCountByFuelElectricName = "Elektrikli Araç Sayısı";
            }

            return View();
        }
    }
}
