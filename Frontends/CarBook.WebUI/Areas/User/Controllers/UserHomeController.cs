using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Home")]
    public class UserHomeController : UserBaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Kullanıcı Ana Sayfa";
            ViewBag.MainPageTitle = "Ana Sayfa";
            ViewBag.SubPageTitle = "Dashboard";

            return View();
        }
    }
}
