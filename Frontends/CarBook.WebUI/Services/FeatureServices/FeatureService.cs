using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultFeatureDto>> GetFeatureAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage responseMessage = await client.GetAsync("features");

            List<ResultFeatureDto> values = new List<ResultFeatureDto>();

            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonData = await responseMessage.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultFeatureDto>
            {
                HttpResponseMessage = responseMessage,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultFeatureDto>> GetFeatureByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"features/{id}");

            ResultFeatureDto value = new ResultFeatureDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultFeatureDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultFeatureDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }
        public async Task<HttpResponseMessage> CreateFeatureAsync(CreateFeatureDto createFeatureDtos)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateFeatureDto>("features", createFeatureDtos);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateFeatureAsync(UpdateFeatureDto updateFeatureDtos)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateFeatureDto>("features", updateFeatureDtos);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteFeatureAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"features/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"features?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
