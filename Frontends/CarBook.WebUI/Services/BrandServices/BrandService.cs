using CarBook.Dto.BrandDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Windows.Markup;

namespace CarBook.WebUI.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultBrandDto>> GetBrandAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("brands");

            List<ResultBrandDto> values = new List<ResultBrandDto>();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBrandDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultBrandDto>> GetBrandByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"brands/{id}");

            ResultBrandDto value = new ResultBrandDto();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultBrandDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultBrandDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }
        public async Task<HttpResponseMessage> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateBrandDto>("brands", createBrandDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateBrandDto>("brands", updateBrandDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteBrandAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"brands/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"brands?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
