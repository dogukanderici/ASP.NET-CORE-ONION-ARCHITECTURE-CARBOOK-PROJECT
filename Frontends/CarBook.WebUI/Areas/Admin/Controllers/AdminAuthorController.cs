using CarBook.Dto.AuthorDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.AuthorServices;
using CarBook.WebUI.Utilities.FileOperations;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Author")]
    public class AdminAuthorController : AdminBaseController
    {
        private readonly IAuthorService _authorService;
        private readonly IFileOperationHelper _fileOperationHelper;

        public AdminAuthorController(IAuthorService authorService, IFileOperationHelper fileOperationHelper)
        {
            _authorService = authorService;
            _fileOperationHelper = fileOperationHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultAuthorDto> serviceResult = await _authorService.GetAuthorAsync();

            AdminUIAuthorViewModel model = new AdminUIAuthorViewModel();

            if (serviceResult.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResult.ResponseDatas;
            }
            else
            {
                ViewBag.UIErrorCode = serviceResult.HttpResponseMessage.StatusCode;
                ViewBag.UIErrorMessage = serviceResult.HttpResponseMessage.Content;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAuthor(AdminUIAuthorViewModel adminUIAuthorViewModel)
        {
            string imageUrl = await _fileOperationHelper.CopyFileToFolder(new FileProperty
            {
                FilePath = "/wwwroot/assets/authors/",
                LoadedFile = adminUIAuthorViewModel.CreateData.Image
            });

            adminUIAuthorViewModel.CreateData.ImageUrl = imageUrl;

            HttpResponseMessage serviceResponse = await _authorService.CreateAuthorAsync(adminUIAuthorViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.Content;
            }

            return View(adminUIAuthorViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateAuthor(Guid id)
        {
            UIServiceApiResponseSetting<ResultAuthorDto> serviceResponse = await _authorService.GetAuthorByIdAsync(id);

            AdminUIAuthorViewModel model = new AdminUIAuthorViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateAuthorDto value = JsonConvert.DeserializeObject<UpdateAuthorDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAuthor(AdminUIAuthorViewModel adminUIAuthorViewModel)
        {
            if (adminUIAuthorViewModel.UpdateData.Image != null)
            {
                string imageUrl = await _fileOperationHelper.CopyFileToFolder(new FileProperty
                {
                    FilePath = "/wwwroot/assets/authors/",
                    LoadedFile = adminUIAuthorViewModel.UpdateData.Image
                });

                adminUIAuthorViewModel.UpdateData.ImageUrl = imageUrl;
            }

            HttpResponseMessage serviceResponse = await _authorService.UpdateAuthorAsync(adminUIAuthorViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
            }
            else
            {
                ViewBag.UIErrorCode = serviceResponse.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.Content;
            }

            return View(adminUIAuthorViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            HttpResponseMessage serviceResponse = await _authorService.DeleteAuthorAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.UIErrorCode = serviceResponse.StatusCode;
                ViewBag.UIErrorMessage = serviceResponse.Content;
            }

            return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
        }
    }
}
