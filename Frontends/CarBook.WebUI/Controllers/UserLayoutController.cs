using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class UserLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
