using Microsoft.AspNetCore.Authorization;

namespace CarBook.WebAPI.Authorization.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public PermissionRequirement(string role)
        {
            Role = role;
        }
    }
}
