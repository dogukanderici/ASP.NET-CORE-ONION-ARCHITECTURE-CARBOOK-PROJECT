using CarBook.Dto.CarDtos;
using Newtonsoft.Json;

namespace CarBook.WebUI.Services.CarServices
{
    public class CarService : ICarService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultCarDto>> GetCarsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("cars");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return values;
        }

        public async Task<ResultCarDto> GetCarByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"cars/{id}");

            ResultCarDto value = new ResultCarDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultCarDto>(jsonData);
            }

            return value;
        }

        public async Task<List<ResultCarDto>> GetLast5CarsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("cars/getlast5cars");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return values;
        }

        public async Task<List<ResultCarDto>> GetCarForOnlyWithPricing()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("cars/getcarforonlywithpricing");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return values;
        }

        public async Task UpdateCarService(UpdateCarDto updateCarDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateCarDto>("cars", updateCarDto);

            var responseString = await response.Content.ReadAsStringAsync();

            var testDeneme = responseString;
        }
    }
}
