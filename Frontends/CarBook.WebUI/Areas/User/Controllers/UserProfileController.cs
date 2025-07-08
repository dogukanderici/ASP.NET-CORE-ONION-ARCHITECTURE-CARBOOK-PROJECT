using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Areas.User.Models;
using CarBook.WebUI.Services.IdentityServices.ProfileSettingServices;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CarBook.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Profile")]
    public class UserProfileController : UserBaseController
    {
        private readonly IProfileSettingService _profileSettingService;
        private readonly IValidator<ChangePasswordDto> _changePasswordValidator;

        public UserProfileController(IProfileSettingService profileSettingService, IValidator<ChangePasswordDto> changePasswordValidator)
        {
            _profileSettingService = profileSettingService;
            _changePasswordValidator = changePasswordValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Profilim";

            return View();
        }

        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ProfileSettingUIModel profileSettingUIModel)
        {
            ValidationResult validator = _changePasswordValidator.Validate(profileSettingUIModel.ChangePassword);

            if (validator.IsValid)
            {
                bool result = await _profileSettingService.ChangePassword(profileSettingUIModel);
            }
            else
            {
                foreach (var item in validator.Errors)
                {
                    ModelState.AddModelError($"ChangePassword.{item.PropertyName}", item.ErrorMessage);
                }
            }

            return View();
        }
    }
}
