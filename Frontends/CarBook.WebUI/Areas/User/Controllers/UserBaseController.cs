using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.User.Controllers
{
    [Authorize]
    public class UserBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
