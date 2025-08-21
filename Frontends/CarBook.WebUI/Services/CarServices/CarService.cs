using CarBook.Dto.CarDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.CarServices
{
    public class CarService : ICarService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultCarDto>> GetCarsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("cars");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarDto>> GetCarByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"cars/{id}");

            ResultCarDto value = new ResultCarDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultCarDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarDto>> GetLast5CarsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("cars/getlast5cars");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarDto>> GetCarForOnlyWithPricing()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("cars/getcarforonlywithpricing");

            List<ResultCarDto> values = new List<ResultCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<HttpResponseMessage> CreateCarService(CreateCarDto updateCarDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateCarDto>("cars", updateCarDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateCarService(UpdateCarDto updateCarDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateCarDto>("cars", updateCarDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteCarService(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"cars/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"cars?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
