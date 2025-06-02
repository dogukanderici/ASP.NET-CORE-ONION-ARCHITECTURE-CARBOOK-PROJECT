using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarBook.WebUI.Services.IdentityServices.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApiSettings _apiSettings;

        public LoginService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;
            _apiSettings = apiSettings.Value;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("LoginClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<LoginDto>("logins", loginDto);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                TokenResponseDto tokenResponseDto = JsonConvert.DeserializeObject<TokenResponseDto>(jsonData);

                //  Servis sonucu gelen sade claim bilgisi Claims listesine çevrilir.

                List<Claim> claims = tokenResponseDto.Claims.Select(c => new Claim(c.Type, c.Value)).ToList();

                // Kullanıcı bilgilerini tarayıcıda saklayabilmek için kullanılacak olan ClaimPrincipal nesnesi için ClaimIdentity nesnesine dönüştürülür.
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

                // ClaimIdentiy nesnesi ClaimPrincipal nesnesine dönüştürülür.
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Token'ların tarayıcıda nasıl saklancağını belirtir.
                AuthenticationProperties authenticationProperties = new AuthenticationProperties();

                authenticationProperties.StoreTokens(new List<AuthenticationToken>() {
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.AccessToken,
                        Value = tokenResponseDto.AccessToken
                    },
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.RefreshToken,
                        Value = tokenResponseDto.RefreshToken
                    },
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.ExpiresIn,
                        Value=tokenResponseDto.ExpiresIn
                    }
                });

                authenticationProperties.IsPersistent = false;

                await _contextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authenticationProperties
                );

                return true;
            }

            return false;
        }
    }
}
