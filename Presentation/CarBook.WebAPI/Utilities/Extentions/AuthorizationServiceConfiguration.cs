using CarBook.WebAPI.Authorization.Handlers;
using CarBook.WebAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace CarBook.WebAPI.Utilities.Extentions
{
    public static class AuthorizationServiceConfiguration
    {
        public static IServiceCollection AddAuthorizationServices(this IServiceCollection service)
        {
            service.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            service.AddAuthorization(opt =>
            {
                opt.AddPolicy("ReadPermissionPolicy", policy =>
                {
                    policy.AddRequirements(new PermissionRequirement("ReadPermission"));
                });
            });

            service.AddAuthorization(opt =>
            {
                opt.AddPolicy("FullPermissionPolicy", policy =>
                {
                    policy.AddRequirements(new PermissionRequirement("FullPermission"));
                });
            });

            service.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPermissionPolicy", policy =>
                {
                    policy.AddRequirements(new PermissionRequirement("AdminPermission"));
                });
            });

            return service;
        }
    }
}
