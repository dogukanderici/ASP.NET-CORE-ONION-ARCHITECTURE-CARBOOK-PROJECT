using CarBook.Dto.ServiceDtos;
using Newtonsoft.Json;

namespace CarBook.WebUI.Services.OurServiceServices
{
    public class OurServiceService : IOurServiceService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OurServiceService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultServiceDto>> GetServicesAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ServiceClient");
            HttpResponseMessage response = await client.GetAsync("services");

            List<ResultServiceDto> values = new List<ResultServiceDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
            }

            return values;
        }
    }
}
