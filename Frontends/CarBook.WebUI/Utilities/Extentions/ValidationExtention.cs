using CarBook.WebUI.Validators.AboutValidators;
using CarBook.WebUI.Validators.IdentityValidators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CarBook.WebUI.Utilities.Extentions
{
    public static class ValidationExtention
    {
        public static IServiceCollection AddValidationExtention(this IServiceCollection service)
        {
            // DataAnnotation Devre Dışı Bırakılır.
            service.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            service.AddValidatorsFromAssemblyContaining<LoginValidator>();
            service.AddValidatorsFromAssemblyContaining<RegisterValidator>();
            service.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateAboutValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateAboutValidator>();


            return service;
        }
    }
}
