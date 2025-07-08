using CarBook.WebAPI.Utilities.Helper;

namespace CarBook.WebAPI.Utilities.Extentions
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddCustomDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IValidationResultMessageHelper, ValidationResultMessageHelper>();

            return services;
        }
    }
}
