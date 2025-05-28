using CarBook.Application.Validators.CarReviewValidators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CarBook.WebAPI.Utilities.Extentions
{
    public static class ValidationExtentions
    {
        public static IServiceCollection AddFluentValidations(this IServiceCollection service)
        {
            // DataAnnotation Devre Dışı Bırakılır.
            service.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            service.AddValidatorsFromAssemblyContaining<CreateCarReviewValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateCarReviewValidator>();

            return service;
        }
    }
}
