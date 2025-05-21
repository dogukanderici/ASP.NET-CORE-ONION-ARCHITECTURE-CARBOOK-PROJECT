using CarBook.Dto.BlogCommentDtos;
using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;

namespace CarBook.WebUI.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public BlogController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.PageRouteTitle = "Bloglar"; var client = _httpClientFactory.CreateClient();

            BlogUIViewModel model = new BlogUIViewModel();

            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/blogs/getblogtotalcount?publishstate=true");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<int>(jsonData);

                model.TotalData = value;
            }

            return View(model);
        }

        [HttpPost("IndexPartial")]
        public async Task<PartialViewResult> IndexPartial(int pageDataSize, int pageNumber)
        {
            var queryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());

            queryString["publishState"] = "true";
            queryString["pageDataSize"] = pageDataSize.ToString();
            queryString["pageNumber"] = pageNumber.ToString();

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/blogs/getblogwithpublishstate?{queryString}");

            BlogUIViewModel model = new BlogUIViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);

                model.BlogDatas = value;
            }

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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateBlogCommentDto>($"{_apiSettings.ApiBaseUrl}/blogcomments", blogUIViewModel.CreateBlogCommentData);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BlogDetail", "Blog", new { id = blogUIViewModel.CreateBlogCommentData.BlogID });
            }

            return RedirectToAction("Index", "Blog");
        }
    }
}
