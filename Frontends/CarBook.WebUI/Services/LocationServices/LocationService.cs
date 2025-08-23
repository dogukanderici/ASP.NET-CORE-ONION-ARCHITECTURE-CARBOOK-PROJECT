using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultLocationDto>> GetLocationAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("locations");

            List<ResultLocationDto> values = new List<ResultLocationDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultLocationDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultLocationDto>> GetLocationByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"locations/{id}");

            ResultLocationDto value = new ResultLocationDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultLocationDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultLocationDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> CreateLocationAsync(CreateLocationDto createLocationDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateLocationDto>("locations", createLocationDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateLocationAsync(UpdateLocationDto updateLocationDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateLocationDto>("locations", updateLocationDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteLocationAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"locations/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"locations?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
