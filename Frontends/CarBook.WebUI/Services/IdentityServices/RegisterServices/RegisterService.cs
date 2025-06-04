using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace CarBook.WebUI.Services.IdentityServices.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("RegisterClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<RegisterDto>("registers", registerDto);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
