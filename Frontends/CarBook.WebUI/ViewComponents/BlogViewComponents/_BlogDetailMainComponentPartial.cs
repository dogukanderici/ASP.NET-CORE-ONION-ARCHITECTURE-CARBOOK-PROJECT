using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.BlogServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailMainComponentPartial : ViewComponent
    {
        private readonly IBlogService _blogService;

        public _BlogDetailMainComponentPartial(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            UIServiceApiResponseSetting<ResultBlogDto> serviceResponse = await _blogService.GetBlogByIdAsync(id);

            BlogUIViewModel model = new BlogUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.BlogByIdData = serviceResponse.ResponseData;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }
    }
}
