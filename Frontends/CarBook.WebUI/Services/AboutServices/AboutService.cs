using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CarBook.WebUI.Services.AboutServices
{
    public class AboutService : IAboutService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AboutService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIServiceApiResponseSetting<ResultAboutDto>> GetAboutAsync()
        {

            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");

            HttpResponseMessage response = await client.GetAsync("abouts");

            List<ResultAboutDto> values = new List<ResultAboutDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            }

            return new UIServiceApiResponseSetting<ResultAboutDto>
            {
                ResponseDatas = values,
                HttpResponseMessage = response
            };
        }

        public async Task<UIServiceApiResponseSetting<ResultAboutDto>> GetAboutByIdAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ReadOnlyClient");
            HttpResponseMessage response = await client.GetAsync($"abouts/{id}");

            string responseData = await response.Content.ReadAsStringAsync();

            ResultAboutDto value = new ResultAboutDto();

            if (response.IsSuccessStatusCode)
            {
                value = JsonConvert.DeserializeObject<ResultAboutDto>(responseData);
            }

            return new UIServiceApiResponseSetting<ResultAboutDto>
            {
                ResponseData = value,
                HttpResponseMessage = response
            };
        }

        public async Task<HttpResponseMessage> CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<CreateAboutDto>("abouts", createAboutDto);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateAboutDto>("abouts", updateAboutDto);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAboutAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");
            HttpResponseMessage response = await client.GetAsync($"abouts/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ResultAboutDto result = JsonConvert.DeserializeObject<ResultAboutDto>(responseData);

                if (result != null)
                {
                    HttpResponseMessage deleteDataResponse = await client.DeleteAsync($"abouts?id={id}");

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
