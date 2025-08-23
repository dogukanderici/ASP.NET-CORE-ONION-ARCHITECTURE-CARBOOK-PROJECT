using CarBook.Dto.BlogCategoryDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.BlogCategoryServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailSideBarCategoryListComponentPartial : ViewComponent
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public _BlogDetailSideBarCategoryListComponentPartial(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UIServiceApiResponseSetting<ResultBlogCategoryDto> serviceResponse = await _blogCategoryService.GetBlogCategoryAsync();

            BlogCategoryUIViewModel model = new BlogCategoryUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.BlogCategoryDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }
    }
}
