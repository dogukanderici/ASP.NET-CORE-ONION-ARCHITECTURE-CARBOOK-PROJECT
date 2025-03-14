using CarBook.Dto.StatisticsDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Statistics")]
    public class AdminStatisticsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminStatisticsController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var getAuthorCountResponseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetAuthorCount");
            if (getAuthorCountResponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await getAuthorCountResponseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.AuthorCount = value.AuthorCount;
                ViewBag.AuthorCountName = "Yazar Sayısı";
            }

            var getAvgRentPriceForDailyCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetAvgRentPriceForDailyCount");
            if (getAvgRentPriceForDailyCount.IsSuccessStatusCode)
            {
                var jsonData = await getAvgRentPriceForDailyCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.AvgRentPriceForDailyCount = value.AvgRentPriceForDailyCount;
                ViewBag.AvgRentPriceForDailyCountName = "Günlük Ort. Fiyat";
            }

            var getAvgRentPriceForHourCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetAvgRentPriceForHourlyCount");
            if (getAvgRentPriceForHourCount.IsSuccessStatusCode)
            {
                var jsonData = await getAvgRentPriceForHourCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.AvgRentPriceForHourlyCount = value.AvgRentPriceForHourlyCount;
                ViewBag.AvgRentPriceForHourlyCountName = "Saatlik Ort. Fiyat";
            }

            var getAvgRentPriceForMounthlyCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetAvgRentPriceForMountlyCount");
            if (getAvgRentPriceForMounthlyCount.IsSuccessStatusCode)
            {
                var jsonData = await getAvgRentPriceForMounthlyCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.AvgRentPriceForMountlyCount = value.AvgRentPriceForMountlyCount;
                ViewBag.AvgRentPriceForMountlyCountName = "Aylık Ort. Fiyat";
            }

            var getAvgRentPriceForWeeklyCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetAvgRentPriceForWeeklyCount");
            if (getAvgRentPriceForWeeklyCount.IsSuccessStatusCode)
            {
                var jsonData = await getAvgRentPriceForWeeklyCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.AvgRentPriceForWeeklyCount = value.AvgRentPriceForWeeklyCount;
                ViewBag.AvgRentPriceForWeeklyCountName = "Haftalık Ort. Fiyat";
            }

            var getBlogCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetBlogCount");
            if (getBlogCount.IsSuccessStatusCode)
            {
                var jsonData = await getBlogCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.BlogCount = value.BlogCount;
                ViewBag.BlogCountName = "Blog Sayısı";
            }

            var getBlogTitleByMaxBlogComment = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetBlogTitleByMaxBlogComment");
            if (getBlogTitleByMaxBlogComment.IsSuccessStatusCode)
            {
                var jsonData = await getBlogTitleByMaxBlogComment.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.BlogTitleByMaxBlogComment = value.BlogTitleByMaxBlogComment;
                ViewBag.BlogTitleByMaxBlogCommentName = "En Fazla Yoruma Sahip Blog";
            }

            var getBrandCount = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetBrandCount");
            if (getBrandCount.IsSuccessStatusCode)
            {
                var jsonData = await getBrandCount.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.BrandCount = value.BrandCount;
                ViewBag.BrandCountName = "Marka Sayısı";
            }

            var getBrandNameByMaxCar = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetBrandNameByMaxCar");
            if (getBrandNameByMaxCar.IsSuccessStatusCode)
            {
                var jsonData = await getBrandNameByMaxCar.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.BrandNameByMaxCar = value.BrandNameByMaxCar;
                ViewBag.BrandNameByMaxCarName = "En Fazla Araca Sahip Marka";
            }

            var getCarBrandAndModelByPriceDailyMax = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarBrandAndModelByPriceDailyMax");
            if (getCarBrandAndModelByPriceDailyMax.IsSuccessStatusCode)
            {
                var jsonData = await getCarBrandAndModelByPriceDailyMax.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarBrandAndModelByPriceDailyMax = value.CarBrandAndModelByPriceDailyMax;
                ViewBag.CarBrandAndModelByPriceDailyMaxName = "Günlük En Pahalı Araç";
            }

            var getCarBrandAndModelByPriceDailyMin = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarBrandAndModelByPriceDailyMin");
            if (getCarBrandAndModelByPriceDailyMin.IsSuccessStatusCode)
            {
                var jsonData = await getCarBrandAndModelByPriceDailyMin.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarBrandAndModelByPriceDailyMin = value.CarBrandAndModelByPriceDailyMin;
                ViewBag.CarBrandAndModelByPriceDailyMinName = "Günlük En Ucuz Araç";
            }

            var getCarCountByFuelElectric = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarCountByFuelElectric");
            if (getCarCountByFuelElectric.IsSuccessStatusCode)
            {
                var jsonData = await getCarCountByFuelElectric.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCountByFuelElectric = value.CarCountByFuelElectric;
                ViewBag.CarCountByFuelElectricName = "Elektrikli Araç Sayısı";
            }

            var getCarCountByFuelGasOrDiesel = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarCountByFuelGasOrDiesel");
            if (getCarCountByFuelGasOrDiesel.IsSuccessStatusCode)
            {
                var jsonData = await getCarCountByFuelGasOrDiesel.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCountByFuelGasOrDiesel = value.CarCountByFuelGasOrDiesel;
                ViewBag.CarCountByFuelGasOrDieselName = "Benzin/Dizel Araç Sayısı";
            }

            var getCarCountByKmSmallerThan1000 = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarCountByKmSmallerThan1000");
            if (getCarCountByKmSmallerThan1000.IsSuccessStatusCode)
            {
                var jsonData = await getCarCountByKmSmallerThan1000.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCountByKmSmallerThan1000 = value.CarCountByKmSmallerThan1000;
                ViewBag.CarCountByKmSmallerThan1000Name = "1000 KM Düşük Araç Sayısı";
            }

            var getCarCountByTransmissionIsAuto = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/Statistics/GetCarCountByTransmissionIsAuto");
            if (getCarCountByTransmissionIsAuto.IsSuccessStatusCode)
            {
                var jsonData = await getCarCountByTransmissionIsAuto.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCountByTransmissionIsAuto = value.CarCountByTransmissionIsAuto;
                ViewBag.CarCountByTransmissionIsAutoName = "Otomatik Vites Araç Sayısı";
            }

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

            return View();
        }
    }
}
