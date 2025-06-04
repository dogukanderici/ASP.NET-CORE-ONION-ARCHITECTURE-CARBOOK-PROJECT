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
            service.AddHttpClient("AboutReadOnlyClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialsTokenHandler>();
            service.AddHttpClient("AboutClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("BlogClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("BlogCommentClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("BannerClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("LocationClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("CarClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("ServiceClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("TestimonialClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("StatisticsService", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("FooterAddressClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient("SocialMediaClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            #endregion
            return service;
        }
    }
}
