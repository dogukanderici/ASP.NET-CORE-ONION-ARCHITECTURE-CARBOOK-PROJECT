using CarBook.WebAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CarBook.WebAPI.Authorization.Handlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                return Task.CompletedTask;
            }

            bool hasRole = context.User.Claims.Any(c =>
            (c.Type == ClaimTypes.Role ||
            c.Type == "role" ||
            c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"));

            if (hasRole)
            {
                if (context.User.Claims.Any(c =>
                (c.Type == ClaimTypes.Role ||
                c.Type == "role" ||
                c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role") &&
                c.Value.Equals(requirement.Role)))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    return Task.CompletedTask;
                }
            }
            else
            {
                if (context.User.Claims.Any(c => c.Type == "scope" && c.Value.Equals(requirement.Role)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
