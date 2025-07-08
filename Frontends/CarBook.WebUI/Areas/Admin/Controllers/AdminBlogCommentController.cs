using CarBook.Dto.AuthorDtos;
using CarBook.Dto.BlogCommentDtos;
using CarBook.WebUI.Areas.Admin.Models;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBlogCommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("/blogcomments/getblogcommentbyblogid?id={id}");

            AdminUIBlogCommentViewModel model = new AdminUIBlogCommentViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultBlogCommentDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }
    }
}
