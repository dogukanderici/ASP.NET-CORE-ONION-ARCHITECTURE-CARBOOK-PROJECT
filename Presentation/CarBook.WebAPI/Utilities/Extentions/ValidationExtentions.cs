using CarBook.Application.Validators.AboutValidators;
using CarBook.Application.Validators.BannerValidators;
using CarBook.Application.Validators.BlogCategoryValidators;
using CarBook.Application.Validators.BlogCommentValidators;
using CarBook.Application.Validators.BlogTagCloudValidations;
using CarBook.Application.Validators.BlogValidators;
using CarBook.Application.Validators.CarFeaturesValidators;
using CarBook.Application.Validators.CarPricingValidators;
using CarBook.Application.Validators.CarReviewValidators;
using CarBook.Application.Validators.CarValidators;
using CarBook.Application.Validators.ContactValidators;
using CarBook.Application.Validators.FeatureValidators;
using CarBook.Application.Validators.FooterAddressValidators;
using CarBook.Application.Validators.LocationValidators;
using CarBook.Application.Validators.PricingTypeValidators;
using CarBook.Application.Validators.ReservationValidators;
using CarBook.Application.Validators.ServiceValidators;
using CarBook.Application.Validators.SocialMediaValidators;
using CarBook.Application.Validators.TagCloudValidator;
using CarBook.Application.Validators.TestimonialValidators;
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

            service.AddValidatorsFromAssemblyContaining<CreateAboutValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateAboutValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateBannerValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateBannerValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateBlogCategoryValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateBlogCategoryValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateBlogCommentValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateBlogCommentValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateBlogTagCloudValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateBlogTagCloudValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateBlogValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateBlogValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateCarFeatureValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateCarFeatureValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateCarPricingValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateCarPricingValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateCarReviewValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateCarReviewValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateCarValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateCarValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateContactValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateContactValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateFeatureValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateFeatureValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateFooterAddressValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateFooterAddressValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateLocationValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateLocationValidator>();

            service.AddValidatorsFromAssemblyContaining<CreatePricingTypeValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdatePricingTypeValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateReservationValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateReservationValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateServiceValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateServiceValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateSocialMediaValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateSocialMediaValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateTagCloudValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateTagCloudValidator>();

            service.AddValidatorsFromAssemblyContaining<CreateTestimonialValidator>();
            service.AddValidatorsFromAssemblyContaining<UpdateTestimonialValidator>();

            return service;
        }
    }
}
