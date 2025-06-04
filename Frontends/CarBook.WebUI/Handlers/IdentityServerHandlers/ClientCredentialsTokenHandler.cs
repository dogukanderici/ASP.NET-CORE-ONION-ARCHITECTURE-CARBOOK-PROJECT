
using CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialsTokenServices;
using System.Net.Http.Headers;

namespace CarBook.WebUI.Handlers.IdentityServerHandlers
{
    public class ClientCredentialsTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialsTokenService _clientCredentialsTokenService;

        public ClientCredentialsTokenHandler(IClientCredentialsTokenService clientCredentialsTokenService)
        {
            _clientCredentialsTokenService = clientCredentialsTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string checkClientToken = _clientCredentialsTokenService.GetClientCredentialsToken();

            if (string.IsNullOrEmpty(checkClientToken))
            {
                string token = await _clientCredentialsTokenService.SetClientCredentialsToken();
                checkClientToken = token;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", checkClientToken);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            return response;

        }
    }
}
