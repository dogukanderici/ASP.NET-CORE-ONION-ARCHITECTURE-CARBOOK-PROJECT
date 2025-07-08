using CarBook.Dto.RentACarDtos;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace CarBook.WebUI.Services.RentACarServices
{
    public class RentACarService : IRentACarService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RentACarService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultRentACarDto>> GetRentACarWithAvailablity(NameValueCollection query)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"RentACars/GetRentACarWithAvailablity?{query}");

            List<ResultRentACarDto> values = new List<ResultRentACarDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultRentACarDto>>(jsonData);
            }

            return values;
        }
    }
}
