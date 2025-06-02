using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.AuthUIViewComponents
{
    public class _AuthScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
