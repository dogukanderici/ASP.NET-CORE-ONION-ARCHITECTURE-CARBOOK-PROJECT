using CarBook.Dto.BlogCategoryDtos;
using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Blog")]
    public class AdminBlogController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBlogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("blogs");

            AdminUIBlogViewModel model = new AdminUIBlogViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateBlog()
        {
            List<SelectListItem> blogCategoryList = await GetBlogCategoryAsync();
            ViewBag.BlogCategoryList = blogCategoryList;

            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBlog(AdminUIBlogViewModel adminUIBlogViewModel)
        {
            adminUIBlogViewModel.CreateData.CreatedDate = new DateTimeOffset(DateTime.Now, TimeSpan.FromHours(3));
            adminUIBlogViewModel.CreateData.CoverImageUrl = "deneme";
            adminUIBlogViewModel.CreateData.AuthorID = Guid.Parse("D471ED04-AAFC-48D9-FA7F-08DD572FE794");

            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<CreateBlogDto>("blogs", adminUIBlogViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
            }

            return View(adminUIBlogViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBlog(Guid id)
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync($"blogs/{id}");

            AdminUIBlogViewModel model = new AdminUIBlogViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBlogDto>(jsonData);

                List<SelectListItem> blogCategoryList = await GetBlogCategoryAsync();
                ViewBag.BlogCategoryList = blogCategoryList;

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBlog(AdminUIBlogViewModel adminUIBlogViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PutAsJsonAsync<UpdateBlogDto>("blogs", adminUIBlogViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
            }

            return View(adminUIBlogViewModel);
        }


        [HttpGet("ChangePublishState")]
        public async Task<IActionResult> ChangePublishState(Guid id, bool state)
        {
            state = !!state;

            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.GetAsync($"blogs/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBlogDto>(jsonData);

                value.PublishState = state;

                var responseMessageForUpdate = await client.PutAsJsonAsync<UpdateBlogDto>("blogs", value);

                if (responseMessageForUpdate.IsSuccessStatusCode)
                {
                    var apiMessage = await responseMessageForUpdate.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.DeleteAsync($"blogs?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
        }

        private async Task<List<SelectListItem>> GetBlogCategoryAsync()
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("blogcategories");

            List<SelectListItem> blogCategoryList = new List<SelectListItem>();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultBlogCategoryDto>>(jsonData);

                blogCategoryList = (from item in value
                                    select new SelectListItem
                                    {
                                        Text = item.Name,
                                        Value = item.BlogCategoryID.ToString()
                                    }).ToList();
            }

            return blogCategoryList;
        }
    }
}
