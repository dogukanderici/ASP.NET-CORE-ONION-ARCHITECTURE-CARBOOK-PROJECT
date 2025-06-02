using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Utilities.Settings;
using Newtonsoft.Json;

namespace CarBook.WebUI.Services.AboutServices
{
    public class AboutService : IAboutService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AboutService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultAboutDto>> GetAboutAsync()
        {

            HttpClient client = _httpClientFactory.CreateClient("AboutClient");

            HttpResponseMessage response = await client.GetAsync("abouts");

            List<ResultAboutDto> values = new List<ResultAboutDto>();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            }

            return values;
        }
    }
}
