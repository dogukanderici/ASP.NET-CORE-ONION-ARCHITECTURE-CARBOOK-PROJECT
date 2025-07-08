using CarBook.WebUI.Handlers.IdentityServerHandlers;
using CarBook.WebUI.Services.AboutServices;
using CarBook.WebUI.Services.BannerServices;
using CarBook.WebUI.Services.BlogCommentServices;
using CarBook.WebUI.Services.BlogServices;
using CarBook.WebUI.Services.IdentityServices.LoginServices;
using CarBook.WebUI.Services.LocationServices;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Utilities.Extentions
{
    public static class HttpClientServiceConfigurationExtention
    {
        public static IServiceCollection AddHttpClientConfiguration(this IServiceCollection service, string apiBaseUrl, string identityServerBaseUrl)
        {
            // IHttpClientFactory kullanıldığından konfigürasyonlar HttpClient'ten farklı olarak yapılır.

            // Identity Services
            #region            
            service.AddHttpClient("LoginClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); });
            service.AddHttpClient("RegisterClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); });
            service.AddHttpClient("CredentialClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); });
            #endregion

            // Other Services
            #region
            service.AddHttpClient("ReadOnlyClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();

            service.AddHttpClient("FullAuthClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            #endregion
            return service;
        }
    }
}
