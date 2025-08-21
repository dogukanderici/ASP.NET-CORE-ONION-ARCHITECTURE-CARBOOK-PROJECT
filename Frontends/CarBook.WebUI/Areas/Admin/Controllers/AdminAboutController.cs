using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.AboutServices;
using CarBook.WebUI.Utilities.FileOperations;
using CarBook.WebUI.Utilities.Settings;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    public class AdminAboutController : AdminBaseController
    {
        private readonly IAboutService _aboutService;
        private readonly IValidator<CreateAboutDto> _createAboutValidator;
        private readonly IValidator<UpdateAboutDto> _updateAboutValidator;
        private readonly IFileOperationHelper _fileOperationHelper;

        public AdminAboutController(IAboutService aboutService, IValidator<CreateAboutDto> createAboutValidator, IValidator<UpdateAboutDto> updateAboutValidator, IFileOperationHelper fileOperationHelper)
        {
            _aboutService = aboutService;
            _createAboutValidator = createAboutValidator;
            _updateAboutValidator = updateAboutValidator;
            _fileOperationHelper = fileOperationHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultAboutDto> serviceResult = await _aboutService.GetAboutAsync();

            AdminUIAboutViewModel model = new AdminUIAboutViewModel();

            model.ResultDatas = serviceResult.ResponseDatas;

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAbout(AdminUIAboutViewModel adminUIAboutViewModel)
        {
            ValidationResult validator = _createAboutValidator.Validate(adminUIAboutViewModel.CreateData);

            if (validator.IsValid)
            {
                string imageUrl = await _fileOperationHelper.CopyFileToFolder(new FileProperty
                {
                    FilePath = "/wwwroot/assets/images/",
                    LoadedFile = adminUIAboutViewModel.CreateData.Image
                });

                adminUIAboutViewModel.CreateData.ImageURL = imageUrl;

                HttpResponseMessage createDataResponse = await _aboutService.CreateAboutAsync(adminUIAboutViewModel.CreateData);
                string apiMessage = await createDataResponse.Content.ReadAsStringAsync();

                return createDataResponse.IsSuccessStatusCode ? RedirectToAction("Index", "AdminAbout", new { area = "Admin" }) : View(adminUIAboutViewModel);
            }
            else
            {
                foreach (var item in validator.Errors)
                {
                    ModelState.AddModelError($"CreateData.{item.PropertyName}", item.ErrorMessage);
                }
            }

            return View(adminUIAboutViewModel);

        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            UIServiceApiResponseSetting<ResultAboutDto> serviceResult = await _aboutService.GetAboutByIdAsync(id);

            AdminUIAboutViewModel model = new AdminUIAboutViewModel();

            if (serviceResult.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResult.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateAboutDto value = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);

                model.UpdateData = value;

                return View(model);
            }
            else
            {
                ViewBag.ErrorCode = serviceResult.HttpResponseMessage.StatusCode;
            }

            return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAbout(AdminUIAboutViewModel adminUIAboutViewModel)
        {
            ValidationResult validator = _updateAboutValidator.Validate(adminUIAboutViewModel.UpdateData);

            if (validator.IsValid)
            {
                if (adminUIAboutViewModel.UpdateData.Image != null)
                {
                    string imageUrl = await _fileOperationHelper.CopyFileToFolder(new FileProperty
                    {
                        FilePath = "/wwwroot/assets/images/",
                        LoadedFile = adminUIAboutViewModel.UpdateData.Image
                    });

                    adminUIAboutViewModel.UpdateData.ImageURL = imageUrl;
                }

                HttpResponseMessage updateDataResponse = await _aboutService.UpdateAboutAsync(adminUIAboutViewModel.UpdateData);

                if (updateDataResponse.IsSuccessStatusCode)
                {
                    string apiMessage = await updateDataResponse.Content.ReadAsStringAsync();

                    return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
                }
            }
            else
            {
                foreach (var item in validator.Errors)
                {
                    ModelState.AddModelError($"UpdateData.{item.PropertyName}", item.ErrorMessage);
                }
            }

            return View(adminUIAboutViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            HttpResponseMessage response = await _aboutService.DeleteAboutAsync(id);

            if (response.IsSuccessStatusCode)
            {
                var apiMessage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                ViewBag.ErrorCode = response.StatusCode;
            }

            return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
        }
    }
}
