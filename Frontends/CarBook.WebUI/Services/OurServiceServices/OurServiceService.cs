using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Utilities.Settings;
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

        public async Task<UIServiceApiResponseSetting<ResultServiceDto>> GetServiceAsync()
        {

            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");

            HttpResponseMessage response = await client.GetAsync("services");

            List<ResultServiceDto> values = new List<ResultServiceDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultServiceDto>
            {
                ResponseDatas = values,
                HttpResponseMessage = response
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultServiceDto>> GetServiceByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"services/{id}");

            string responseData = await response.Content.ReadAsStringAsync();

            ResultServiceDto value = new ResultServiceDto();

            if (response.IsSuccessStatusCode)
            {
                value = JsonConvert.DeserializeObject<ResultServiceDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultServiceDto>
            {
                ResponseData = value,
                HttpResponseMessage = response
            };
        }

        public async Task<HttpResponseMessage> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateServiceDto>("services", createServiceDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateServiceAsync(UpdateServiceDto updateServiceDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateServiceDto>("services", updateServiceDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteServiceAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"services/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ResultServiceDto result = JsonConvert.DeserializeObject<ResultServiceDto>(responseData);

                if (result != null)
                {
                    HttpResponseMessage deleteDataResponse = await client.DeleteAsync($"services?id={id}");

                    return deleteDataResponse;
                }
                else
                {
                    return response;
                }
            }
            else
            {
                return response;
            }
        }
    }
}
