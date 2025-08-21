using CarBook.Dto.BlogCategoryDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.BlogCategoryServices;
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
        private readonly IBlogCategoryService _blogCategoryService;

        public AdminBlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultBlogCategoryDto> serviceResponse = await _blogCategoryService.GetBlogCategoryAsync();

            AdminUIBlogCategoryViewModel model = new AdminUIBlogCategoryViewModel();

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

        [HttpGet("Create")]
        public IActionResult CreateBlogCategory()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBlogCategory(AdminUIBlogCategoryViewModel adminUIBlogCategoryViewModel)
        {
            HttpResponseMessage serviceResponse = await _blogCategoryService.CreateBlogCategoryAsync(adminUIBlogCategoryViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.Content;
            }

            return View(adminUIBlogCategoryViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBlogCategory(int id)
        {
            UIServiceApiResponseSetting<ResultBlogCategoryDto> serviceResponse = await _blogCategoryService.GetBlogCategoryByIdAsync(id);

            AdminUIBlogCategoryViewModel model = new AdminUIBlogCategoryViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateBlogCategoryDto value = JsonConvert.DeserializeObject<UpdateBlogCategoryDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBlogCategory(AdminUIBlogCategoryViewModel adminUIBlogCategoryViewModel)
        {
            HttpResponseMessage serviceResponse = await _blogCategoryService.UpdateBlogCategoryAsync(adminUIBlogCategoryViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.Content;
            }

            return View(adminUIBlogCategoryViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            HttpResponseMessage serviceResponse = await _blogCategoryService.DeleteBlogCategoryAsync(id);

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.Content;
            }

            return RedirectToAction("Index", "AdminBlogCategory", new { area = "Admin" });
        }
    }
}
