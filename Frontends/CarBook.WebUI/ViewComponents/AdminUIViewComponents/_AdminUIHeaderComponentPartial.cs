using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.AdminUIViewComponents
{
    public class _AdminUIHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string username = HttpContext.User?.FindFirst("fullname")?.Value;
            ViewBag.AdminUserName = username;

            return View();
        }
    }
}
