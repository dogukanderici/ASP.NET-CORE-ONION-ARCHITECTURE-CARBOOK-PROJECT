using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CarBook.WebUI.Services.IdentityServices.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResponseSettings> Login(LoginDto loginDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("LoginClient");
            HttpResponseMessage response = await client.PostAsJsonAsync<LoginDto>("logins", loginDto);

            string jsonData = await response.Content.ReadAsStringAsync();

            ApiResponseSettings responseData = new ApiResponseSettings();

            if (response.IsSuccessStatusCode)
            {
                try
                {
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
                            Value=tokenResponseDto.ExpiresIn.ToString()
                        }
                    });

                    authenticationProperties.IsPersistent = false;

                    await _contextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authenticationProperties
                    );

                    responseData.ResponseState = true;
                    responseData.ResponseMessage = "Login is successful!";

                    return responseData;
                }
                catch (Exception ex)
                {
                    responseData.ResponseState = false;
                    responseData.ResponseMessage = "An error occured while signing!";

                    return responseData;
                }
            }

            responseData = await response.Content.ReadFromJsonAsync<ApiResponseSettings>();

            return responseData;
        }
    }
}
