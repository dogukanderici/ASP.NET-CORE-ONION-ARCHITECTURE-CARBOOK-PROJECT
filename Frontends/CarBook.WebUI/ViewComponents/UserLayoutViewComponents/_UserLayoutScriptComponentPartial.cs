using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
