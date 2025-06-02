using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.AuthUIViewComponents
{
    public class _AuthHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
