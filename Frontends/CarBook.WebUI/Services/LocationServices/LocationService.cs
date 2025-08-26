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

        public async Task<UIServiceApiResponseSetting<ResultLocationDto>> GetLocationAsync(int? skipNumber = null, int? takeNumber = null)
        {
            skipNumber = skipNumber ?? 0;
            takeNumber = takeNumber ?? 0;

            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"locations/{skipNumber}/{takeNumber}");

            ResultLocationDataDto values = new ResultLocationDataDto();
            string jsonDataa = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<ResultLocationDataDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultLocationDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values.Locations,
                TotalDataCount = values.LocationCount
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
