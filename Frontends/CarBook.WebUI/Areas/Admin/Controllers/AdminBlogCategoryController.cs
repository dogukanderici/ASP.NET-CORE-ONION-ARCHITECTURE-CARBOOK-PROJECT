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
    public class AdminBlogCategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AdminBlogCategoryController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/blogcategories");

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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync<CreateBlogCategoryDto>($"{_apiSettings.ApiBaseUrl}/blogcategories", adminUIBlogCategoryViewModel.CreateData);

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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/blogcategories/{id}");

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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync<UpdateBlogCategoryDto>($"{_apiSettings.ApiBaseUrl}/blogcategories", adminUIBlogCategoryViewModel.UpdateData);

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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"{_apiSettings.ApiBaseUrl}/blogcategories?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
        }
    }
}
