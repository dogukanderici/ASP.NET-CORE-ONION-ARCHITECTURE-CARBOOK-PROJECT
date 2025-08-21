using CarBook.Dto.CarFeatureDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.CarFeatureServices
{
    public class CarFeatureService : ICarFeatureService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CarFeatureService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultCarFeatureDto>> GetCarFeatureAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("carfeatures");

            List<ResultCarFeatureDto> values = new List<ResultCarFeatureDto>();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultCarFeatureDto>>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultCarFeatureDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultCarFeatureDto>> GetCarFeatureByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"carfeatures/{id}");

            ResultCarFeatureDto value = new ResultCarFeatureDto();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultCarFeatureDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultCarFeatureDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }
        public async Task<HttpResponseMessage> CreateCarFeatureAsync(List<CreateCarFeatureDto> createCarFeatureDtos)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<List<CreateCarFeatureDto>>("carfeatures/createcarfeaturewithlist", createCarFeatureDtos);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateCarFeatureAsync(UpdateCarFeatureDto updateCarFeatureDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateCarFeatureDto>("carfeatures", updateCarFeatureDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteCarFeatureAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"carfeatures/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"carfeatures?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
