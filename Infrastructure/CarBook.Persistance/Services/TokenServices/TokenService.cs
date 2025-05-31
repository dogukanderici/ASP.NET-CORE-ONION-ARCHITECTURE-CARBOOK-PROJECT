using Azure.Core;
using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using CarBook.Configurations;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistance.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiUrlOptions _apiUrlOptions;
        private readonly IdentityServerOptions _identityServerOptions;

        public TokenService(IHttpClientFactory httpClientFactory, IOptions<ApiUrlOptions> apiUrlOptions, IOptions<IdentityServerOptions> identityServerOptions)
        {
            _httpClientFactory = httpClientFactory;
            _apiUrlOptions = apiUrlOptions.Value;
            _identityServerOptions = identityServerOptions.Value;
        }

        public Task<TokenResponseDto> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseDto> SignIn(LoginDto loginDto)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            DiscoveryDocumentResponse discoveryEndPoint = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _apiUrlOptions.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            PasswordTokenRequest passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _identityServerOptions.CarBookMember.ClientId,
                ClientSecret = _identityServerOptions.CarBookMember.ClientSecret,
                UserName = loginDto.Username,
                Password = loginDto.Password,
                Address = discoveryEndPoint.TokenEndpoint
            };

            TokenResponse token = await client.RequestPasswordTokenAsync(passwordTokenRequest);

            if (token.HttpStatusCode != HttpStatusCode.OK)
            {
                return new TokenResponseDto
                {
                    Status = false,
                    Message = token.ErrorDescription
                };
            }

            // Kullanıcı bilgileri alınır.
            UserInfoRequest userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndPoint.UserInfoEndpoint
            };

            UserInfoResponse userInfo = await client.GetUserInfoAsync(userInfoRequest);

            List<ClaimDto> claims = userInfo.Claims.Select(x => new ClaimDto
            {
                Type = x.Type,
                Value = x.Value,
            }).ToList();

            // ClaimIdentity, ClaimPrincipal ve AuthenticationProperties UI bağımlı kütüphanelerdir. Buradan token bilgileri ve sadece claim bilgileri dönmeli.

            return new TokenResponseDto
            {
                Status = true,
                Message = "Token acquisition process completed successfully!",
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiresIn = DateTime.Now.AddMinutes(token.ExpiresIn).ToString(),
                Claims = claims,
            };
        }

        public async Task<bool> SignUp(RegisterDto registerDto)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            string jsonData = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync($"{_apiUrlOptions.IdentityServerUrl}/api/registers", content);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
