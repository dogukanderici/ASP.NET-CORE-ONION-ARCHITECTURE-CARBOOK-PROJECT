using CarBook.Configurations;
using CarBook.Dto.IdentityDtos;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading.Tasks;

namespace CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialsTokenServices
{
    public class ClientCredentialsTokenService : IClientCredentialsTokenService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientCredentialsTokenService(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
        }

        public string GetClientCredentialsToken()
        {
            string clientToken = _memoryCache.Get<string>(CacheKeys.ClientCredentialToken);

            return clientToken;
        }

        public async Task<string> SetClientCredentialsToken()
        {
            HttpClient client = _httpClientFactory.CreateClient("CredentialClient");
            HttpResponseMessage response = await client.GetAsync("tokens/gettoken");

            TokenResponseDto tokenValue = new TokenResponseDto();

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
                tokenValue = JsonConvert.DeserializeObject<TokenResponseDto>(jsonData);
            }

            _memoryCache.Set(CacheKeys.ClientCredentialToken, tokenValue.AccessToken, TimeSpan.FromSeconds(CacheKeys.TokenExpireInSeconds - 60));

            return tokenValue.AccessToken;
        }
    }
}
