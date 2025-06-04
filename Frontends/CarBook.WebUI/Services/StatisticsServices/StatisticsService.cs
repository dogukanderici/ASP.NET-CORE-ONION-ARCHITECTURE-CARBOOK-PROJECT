using CarBook.Dto.StatisticsDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.Services.StatisticsServices
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StatisticsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> GetCarCountAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("StatisticsService");
            HttpResponseMessage response = await client.GetAsync("Statistics/GetCarCount");

            int carCount = 0;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                ResultStatisticsDto values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                carCount = values.CarCount;
            }

            return carCount == 0 ? 0 : carCount;
        }

        public async Task<int> GetLocationCountAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("StatisticsService");
            HttpResponseMessage response = await client.GetAsync("Statistics/GetLocationCount");

            int locationCount = 0;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                ResultStatisticsDto values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                locationCount = values.LocationCount;
            }

            return locationCount == 0 ? 0 : locationCount;
        }

        public async Task<int> GetBrandCountAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("StatisticsService");
            HttpResponseMessage response = await client.GetAsync("Statistics/GetBrandCount");

            int brandCount = 0;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                ResultStatisticsDto values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                brandCount = values.BrandCount;
            }

            return brandCount == 0 ? 0 : brandCount;
        }

        public async Task<int> GetCarCountByFuelElectricAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("StatisticsService");
            HttpResponseMessage response = await client.GetAsync("Statistics/GetCarCountByFuelElectric");

            int carCountByFuelElectric = 0;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                ResultStatisticsDto values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                carCountByFuelElectric = values.CarCountByFuelElectric;
            }

            return carCountByFuelElectric == 0 ? 0 : carCountByFuelElectric;
        }
    }
}
