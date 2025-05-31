using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace CarBook.WebUI.Services.IdentityServices.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public RegisterService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.PostAsJsonAsync<RegisterDto>($"{_apiSettings.ApiBaseUrl}/registers", registerDto);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
