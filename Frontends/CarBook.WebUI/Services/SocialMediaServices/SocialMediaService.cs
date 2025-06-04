using CarBook.Dto.SocialMediaDtos;
using Newtonsoft.Json;

namespace CarBook.WebUI.Services.SocialMediaServices
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SocialMediaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultSocialMediaDto>> GetSocialMediaAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient("SocialMediaClient");
            HttpResponseMessage response = await client.GetAsync("socialmedias");

            List<ResultSocialMediaDto> values = new List<ResultSocialMediaDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
            }

            return values;
        }
    }
}
