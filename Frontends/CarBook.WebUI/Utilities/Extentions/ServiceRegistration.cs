using CarBook.WebUI.Handlers.IdentityServerHandlers;
using CarBook.WebUI.Services.AboutServices;
using CarBook.WebUI.Services.BannerServices;
using CarBook.WebUI.Services.BlogCommentServices;
using CarBook.WebUI.Services.BlogServices;
using CarBook.WebUI.Services.CarServices;
using CarBook.WebUI.Services.FooterAddressServices;
using CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialsTokenServices;
using CarBook.WebUI.Services.IdentityServices.IdentityServerServices.ClientCredentialTokenServices;
using CarBook.WebUI.Services.IdentityServices.LoginServices;
using CarBook.WebUI.Services.IdentityServices.RegisterServices;
using CarBook.WebUI.Services.LocationServices;
using CarBook.WebUI.Services.OurServiceServices;
using CarBook.WebUI.Services.SocialMediaServices;
using CarBook.WebUI.Services.StatisticsServices;
using CarBook.WebUI.Services.TestimonialServices;

namespace CarBook.WebUI.Utilities.Extentions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDIServices(this IServiceCollection service)
        {
            // Identity Services
            #region 
            service.AddScoped<IResourceOwnerPasswordTokenService, ResourceOwnerPasswordTokenService>();
            service.AddScoped<IClientCredentialsTokenService, ClientCredentialsTokenService>();
            service.AddScoped<ResourceOwnerPasswordTokenHandler>();
            service.AddScoped<ClientCredentialsTokenHandler>();
            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<IRegisterService, RegisterService>();
            #endregion

            // Other Services
            #region
            service.AddScoped<IAboutService, AboutService>();
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<IBlogCommentService, BlogCommentService>();
            service.AddScoped<IBannerService, BannerService>();
            service.AddScoped<ILocationService, LocationService>();
            service.AddScoped<ICarService, CarService>();
            service.AddScoped<IOurServiceService, OurServiceService>();
            service.AddScoped<ITestimonialService, TestimonialService>();
            service.AddScoped<IStatisticsService, StatisticsService>();
            service.AddScoped<IFooterAddressService, FooterAddressService>();
            service.AddScoped<ISocialMediaService, SocialMediaService>();

            #endregion

            return service;
        }
    }
}
