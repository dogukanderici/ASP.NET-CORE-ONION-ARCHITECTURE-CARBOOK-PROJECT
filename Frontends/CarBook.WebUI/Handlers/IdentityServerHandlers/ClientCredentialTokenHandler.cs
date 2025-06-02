
using CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices;
using System.Net;
using System.Net.Http.Headers;

namespace CarBook.WebUI.Handlers.IdentityServerHandlers
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string accessToken = await _clientCredentialTokenService.GetTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue("Header", accessToken);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Refresh Token alınıp Bearer'a yazdırılacak.
            }

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception("Beklenmedik Hata! Kod:500-InternalServerError");
            }

            return response;
        }
    }
}
