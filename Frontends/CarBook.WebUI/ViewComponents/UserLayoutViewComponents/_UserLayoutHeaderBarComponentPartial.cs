using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutHeaderBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
