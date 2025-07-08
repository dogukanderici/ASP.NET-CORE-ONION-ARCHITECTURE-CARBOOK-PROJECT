using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Home")]
    public class AdminHomeController : AdminBaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
