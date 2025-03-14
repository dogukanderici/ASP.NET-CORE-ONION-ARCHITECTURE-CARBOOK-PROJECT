using CarBook.Application.Features.CQRS.Handlers.AboutHandlers;
using CarBook.Application.Features.CQRS.Handlers.BannerHandlers;
using CarBook.Application.Features.CQRS.Handlers.BlogCategoryHandlers;
using CarBook.Application.Features.CQRS.Handlers.BrandHandlers;
using CarBook.Application.Features.CQRS.Handlers.CarFeatureHandlers;
using CarBook.Application.Features.CQRS.Handlers.CarHandlers;
using CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers;
using CarBook.Application.Features.CQRS.Handlers.ContactHandlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services
{
    public static class CQRSServiceRegistration
    {
        public static void CQRSApplicationService(this IServiceCollection services)
        {
            // About
            services.AddScoped<GetAboutQueryHandler>();
            services.AddScoped<GetAboutByIdQueryHandler>();
            services.AddScoped<CreateAboutCommandHandler>();
            services.AddScoped<UpdateAboutCommandHandler>();
            services.AddScoped<RemoveAboutCommandHandler>();

            //Banner
            services.AddScoped<GetBannerQueryHandler>();
            services.AddScoped<GetBannerByIdQueryHandler>();
            services.AddScoped<CreateBannerCommandHandler>();
            services.AddScoped<UpdateBannerCommandHandler>();
            services.AddScoped<RemoveBannerCommandHandler>();

            // Brands
            services.AddScoped<GetBrandQueryHandler>();
            services.AddScoped<GetBrandByIdQueryHandler>();
            services.AddScoped<CreateBrandCommandHandler>();
            services.AddScoped<UpdateBrandCommandHandler>();
            services.AddScoped<RemoveBrandCommandHandler>();

            // Car
            services.AddScoped<GetCarQueryHandler>();
            services.AddScoped<GetLast5CarsQueryHandler>();
            services.AddScoped<GetCarByIdQueryHandler>();
            services.AddScoped<CreateCarCommandHandler>();
            services.AddScoped<UpdateCarCommandHandler>();
            services.AddScoped<RemoveCarCommandHandler>();

            // Blog Category
            services.AddScoped<GetBlogCategoryQueryHandler>();
            services.AddScoped<GetBlogCategoryByIdQueryHandler>();
            services.AddScoped<CreateBlogCategoryCommandHandler>();
            services.AddScoped<UpdateBlogCategoryCommandHandler>();
            services.AddScoped<RemoveBlogCategoryCommandHandler>();

            // Contact
            services.AddScoped<GetContactQueryHandler>();
            services.AddScoped<GetContactInboxOutboxQueryHandler>();
            services.AddScoped<GetContactByIdQueryHandler>();
            services.AddScoped<CreateContactCommandHandler>();
            services.AddScoped<UpdateContactCommandHandler>();
            services.AddScoped<RemoveContactCommandHandler>();

            // CarPricing
            services.AddScoped<GetCarPricingQueryHandler>();
            services.AddScoped<GetCarPricingByIdQueryHandler>();
            services.AddScoped<GetCarPricingByCarIdQueryHandler>();
            services.AddScoped<CreateCarPricingCommandHandler>();
            services.AddScoped<UpdateCarPricingCommandHandler>();
            services.AddScoped<RemoveCarPricingCommandHandler>();

            // CarFeature
            services.AddScoped<GetCarFeatureQueryHandler>();
            services.AddScoped<GetCarFeatureByIdQueryHandler>();
            services.AddScoped<CreateCarFeatureCommandHandler>();
            services.AddScoped<UpdateCarFeatureCommandHandler>();
            services.AddScoped<RemoveCarFeatureCommandHandler>();
        }
    }
}
