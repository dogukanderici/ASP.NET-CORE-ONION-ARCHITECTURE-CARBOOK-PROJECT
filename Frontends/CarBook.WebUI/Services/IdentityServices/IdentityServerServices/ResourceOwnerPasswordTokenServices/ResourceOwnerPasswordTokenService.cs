

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices
{
    public class ResourceOwnerPasswordTokenService : IResourceOwnerPasswordTokenService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ResourceOwnerPasswordTokenService(IHttpContextAccessor contextAccessor)
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
