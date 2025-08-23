using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Services.SocialMediaServices;
using CarBook.WebUI.Utilities.Settings;
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

        public async Task<UIServiceApiResponseSetting<ResultSocialMediaDto>> GetSocialMediaAsync()
        {

            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");

            HttpResponseMessage response = await client.GetAsync("socialmedias");

            List<ResultSocialMediaDto> values = new List<ResultSocialMediaDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultSocialMediaDto>
            {
                ResponseDatas = values,
                HttpResponseMessage = response
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultSocialMediaDto>> GetSocialMediaByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"socialmedias/{id}");

            string responseData = await response.Content.ReadAsStringAsync();

            ResultSocialMediaDto value = new ResultSocialMediaDto();

            if (response.IsSuccessStatusCode)
            {
                value = JsonConvert.DeserializeObject<ResultSocialMediaDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultSocialMediaDto>
            {
                ResponseData = value,
                HttpResponseMessage = response
            };
        }

        public async Task<HttpResponseMessage> CreateSocialMediaAsync(CreateSocialMediaDto createSocialMediaDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateSocialMediaDto>("socialmedias", createSocialMediaDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateSocialMediaAsync(UpdateSocialMediaDto updateSocialMediaDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateSocialMediaDto>("socialmedias", updateSocialMediaDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteSocialMediaAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"socialmedias/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ResultSocialMediaDto result = JsonConvert.DeserializeObject<ResultSocialMediaDto>(responseData);

                if (result != null)
                {
                    HttpResponseMessage deleteDataResponse = await client.DeleteAsync($"socialmedias?id={id}");

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
