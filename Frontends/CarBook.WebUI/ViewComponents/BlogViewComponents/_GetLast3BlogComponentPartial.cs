using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.BlogServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _GetLast3BlogComponentPartial : ViewComponent
    {
        private readonly IBlogService _blogService;

        public _GetLast3BlogComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultBlogDto> values = await _blogService.GetLast3BlogsAsync();

            BlogUIViewModel model = new BlogUIViewModel();

            model.BlogDatas = values;

            return View(model);
        }
    }
}
