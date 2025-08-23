using CarBook.Dto.CarPricingDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace CarBook.WebUI.Services.CarPricingServices
{
    public class CarPricingService : ICarPricingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarPricingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultCarPricingForCarDto>> GetCarPricingAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"carpricings/getcarpricingbycarid?id={id}");

            List<ResultCarPricingForCarDto> values = new List<ResultCarPricingForCarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarPricingForCarDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarPricingForCarDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarPricingDto>> GetCarPricingByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"carpricings/{id}");

            ResultCarPricingDto value = new ResultCarPricingDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultCarPricingDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultCarPricingDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> CreateCarPricingAsync(CreateCarPricingDto createCarPricingDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateCarPricingDto>("carpricings", createCarPricingDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateCarPricingAsync(UpdateCarPricingDto updateCarPricingDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateCarPricingDto>("carpricings", updateCarPricingDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteCarPricingAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"carpricings/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"carpricings?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
