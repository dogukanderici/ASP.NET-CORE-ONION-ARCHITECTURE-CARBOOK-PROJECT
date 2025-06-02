using CarBook.Dto.LocationDtos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarBook.WebUI.Services.LocationServices
{
    public class LocationService : ILocationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LocationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultLocationDto>> GetLocationsAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("LocationClient");
            HttpResponseMessage response = await client.GetAsync("locations");

            List<ResultLocationDto> values = new List<ResultLocationDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
            }

            return values;
        }
    }
}
