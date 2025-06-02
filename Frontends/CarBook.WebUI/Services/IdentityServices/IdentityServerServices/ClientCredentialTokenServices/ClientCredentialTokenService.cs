

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ClientCredentialTokenService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<string> GetTokenAsync()
        {
            string token = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            return token;
        }
    }
}
