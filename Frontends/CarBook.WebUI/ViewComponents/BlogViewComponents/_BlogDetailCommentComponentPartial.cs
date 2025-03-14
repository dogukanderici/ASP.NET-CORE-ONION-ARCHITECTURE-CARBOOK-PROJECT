using CarBook.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailCommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string blogCommentData)
        {
            BlogUIViewModel model = new BlogUIViewModel();

            if (!String.IsNullOrEmpty(blogCommentData))
            {
                model = JsonConvert.DeserializeObject<BlogUIViewModel>(blogCommentData);
            }

            return View(model);
        }
    }
}
