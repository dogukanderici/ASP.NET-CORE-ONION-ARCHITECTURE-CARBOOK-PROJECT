using CarBook.Dto.BlogCommentDtos;
using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.BlogCommentServices;
using CarBook.WebUI.Services.BlogServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CarBook.WebUI.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCommentService _blogCommentService;

        public BlogController(IBlogService blogService, IBlogCommentService blogCommentService)
        {
            _blogService = blogService;
            _blogCommentService = blogCommentService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.PageRouteTitle = "Bloglar";

            BlogUIViewModel model = new BlogUIViewModel();

            int value = await _blogService.GetBlogCountWithPublishStateAsync(true);

            model.TotalData = value;

            return View(model);
        }

        [HttpPost("IndexPartial")]
        public async Task<PartialViewResult> IndexPartial(int pageDataSize, int pageNumber)
        {
            var queryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());

            queryString["publishState"] = "true";
            queryString["pageDataSize"] = pageDataSize.ToString();
            queryString["pageNumber"] = pageNumber.ToString();

            List<ResultBlogDto> values = await _blogService.GetBlogWithPublishStateAsync(queryString);

            BlogUIViewModel model = new BlogUIViewModel();

            model.BlogDatas = values;

            return PartialView(model);
        }

        [HttpGet("BlogDetail")]
        public IActionResult BlogDetail(Guid id)
        {
            ViewBag.PageRouteTitle = "Blog Detayı";
            ViewBag.BlogId = id;

            return View();
        }

        [HttpPost("PostComment")]
        public async Task<IActionResult> PostBlogComment(BlogUIViewModel blogUIViewModel)
        {
            blogUIViewModel.CreateBlogCommentData.CreatedDate = new DateTimeOffset(DateTime.Now, TimeSpan.FromHours(3));
            bool response = await _blogCommentService.CreateNewBlogCommentAsync(blogUIViewModel.CreateBlogCommentData);

            if (response)
            {
                return RedirectToAction("BlogDetail", "Blog", new { id = blogUIViewModel.CreateBlogCommentData.BlogID });
            }

            return RedirectToAction("Index", "Blog");
        }
    }
}
