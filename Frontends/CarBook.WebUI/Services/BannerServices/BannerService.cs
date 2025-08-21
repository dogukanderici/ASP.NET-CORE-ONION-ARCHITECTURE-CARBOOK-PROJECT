using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;

namespace CarBook.WebUI.Services.BannerServices
{
    public class BannerService : IBannerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BannerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultBannerDto>> GetBannerAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync("banners");

            List<ResultBannerDto> values = new List<ResultBannerDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBannerDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultBannerDto>
            {
                HttpResponseMessage = response,
                ResponseDatas = values
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultBannerDto>> GetBannerByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"banners/{id}");

            ResultBannerDto value = new ResultBannerDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                value = JsonConvert.DeserializeObject<ResultBannerDto>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultBannerDto>
            {
                HttpResponseMessage = response,
                ResponseData = value
            };
        }

        public async Task<HttpResponseMessage> CreateBannerAsync(CreateBannerDto createBannerDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateBannerDto>("banners", createBannerDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateBannerAsync(UpdateBannerDto updateBannerDto)
        {

            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateBannerDto>("banners", updateBannerDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteBannerAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"banners/{id}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpResponseMessage deletedDataResponse = await client.DeleteAsync($"banners?id={id}");

                return deletedDataResponse;
            }

            return response;
        }
    }
}
