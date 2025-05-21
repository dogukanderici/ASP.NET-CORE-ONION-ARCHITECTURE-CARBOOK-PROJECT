using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailLeaveACommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(Guid id)
        {
            ViewBag.BlogId = id;

            return View();
        }
    }
}
