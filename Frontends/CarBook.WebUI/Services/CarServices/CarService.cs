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

        public async Task<List<ResultCarDto>> GetLast5CarsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("CarClient");
            HttpResponseMessage response = await client.GetAsync("cars/getlast5cars");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return values;
        }
    }
}
