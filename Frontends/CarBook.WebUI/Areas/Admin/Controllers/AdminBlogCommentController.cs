using CarBook.Dto.AuthorDtos;
using CarBook.Dto.BlogCommentDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.BlogCommentServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/BlogComment")]
    public class AdminBlogCommentController : AdminBaseController
    {
        private readonly IBlogCommentService _blogCommentService;

        public AdminBlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            UIServiceApiResponseSetting<ResultBlogCommentDto> serviceResponse = await _blogCommentService.GetBlogCommentByIdAsync(id);

            AdminUIBlogCommentViewModel model = new AdminUIBlogCommentViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
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
