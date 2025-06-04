using CarBook.Domain.Entities;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CarBook.IdentityServer.Utilities
{
    public class UserCustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());

            if (user == null)
            {
                return;
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);
            IEnumerable<Claim> roleClaims = roles.Select(role => new Claim("role", role));

            var userInfoClaims = new List<Claim>
            {
                new Claim("email",user.Email),
                new Claim("username",user.UserName),
                new Claim("fullname",$"{user.Name} {user.Surname}")
            };

            context.IssuedClaims.AddRange(roleClaims);
            context.IssuedClaims.AddRange(userInfoClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());

            context.IsActive = user != null;
        }
    }
}