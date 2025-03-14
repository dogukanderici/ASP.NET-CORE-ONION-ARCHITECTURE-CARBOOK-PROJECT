using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailContentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string blogContent)
        {
            BlogUIViewModel model = new BlogUIViewModel();

            if (blogContent != null)
            {
                model = JsonConvert.DeserializeObject<BlogUIViewModel>(blogContent);
            }

            return View(model);
        }
    }
}
