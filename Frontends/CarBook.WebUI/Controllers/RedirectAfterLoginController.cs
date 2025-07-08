using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CarBook.WebUI.Controllers
{
    public class RedirectAfterLoginController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public RedirectAfterLoginController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            bool isUserAdmin = User.IsInRole("AdminPermission");

            string token = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (isUserAdmin)
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }

            return RedirectToAction("Index", "UserHome", new { area = "User" });
        }
    }
}
