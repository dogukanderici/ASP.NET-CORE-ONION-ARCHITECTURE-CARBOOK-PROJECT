using CarBook.Dto.BlogCategoryDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/BlogCategory")]
    public class AdminBlogCategoryController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBlogCategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("blogcategories");

            AdminUIBlogCategoryViewModel model = new AdminUIBlogCategoryViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultBlogCategoryDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateBlogCategory()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBlogCategory(AdminUIBlogCategoryViewModel adminUIBlogCategoryViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<CreateBlogCategoryDto>("blogcategories", adminUIBlogCategoryViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
            }

            return View(adminUIBlogCategoryViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBlogCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync($"blogcategories/{id}");

            AdminUIBlogCategoryViewModel model = new AdminUIBlogCategoryViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBlogCategoryDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBlogCategory(AdminUIBlogCategoryViewModel adminUIBlogCategoryViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PutAsJsonAsync<UpdateBlogCategoryDto>("blogcategories", adminUIBlogCategoryViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
            }

            return View(adminUIBlogCategoryViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            var client = _httpClientFactory.CreateClient(" FullAuthClient");
            var responseMessage = await client.DeleteAsync($"blogcategories?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
        }
    }
}
