using CarBook.Dto.BannerDtos;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public async Task<List<ResultBannerDto>> GetBannerAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("BannerClient");
            HttpResponseMessage response = await client.GetAsync("banners");

            List<ResultBannerDto> values = new List<ResultBannerDto>();

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultBannerDto>>(jsonData);
            }

            return values;
        }
    }
}
