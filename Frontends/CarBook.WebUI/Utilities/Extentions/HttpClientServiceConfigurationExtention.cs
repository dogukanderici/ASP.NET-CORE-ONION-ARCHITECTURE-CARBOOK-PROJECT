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
            service.AddScoped<ILoginService, LoginService>();
            service.AddHttpClient("LoginClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); });
            #endregion

            // Other Services
            #region
            service.AddScoped<IAboutService, AboutService>();
            service.AddHttpClient("AboutClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddScoped<IBlogService, BlogService>();
            service.AddHttpClient("BlogClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddScoped<IBlogCommentService, BlogCommentService>();
            service.AddHttpClient("BlogCommentClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddScoped<IBannerService, BannerService>();
            service.AddHttpClient("BannerClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddScoped<ILocationService, LocationService>();
            service.AddHttpClient("LocationClient", opt => { opt.BaseAddress = new Uri(apiBaseUrl); })
                .AddHttpMessageHandler<ClientCredentialTokenHandler>();

            #endregion
            return service;
        }
    }
}
