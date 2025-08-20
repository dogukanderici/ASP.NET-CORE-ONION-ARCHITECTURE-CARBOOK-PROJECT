using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string username = HttpContext.User?.FindFirst("username")?.Value;
            ViewBag.Username = username;

            return View();
        }
    }
}
