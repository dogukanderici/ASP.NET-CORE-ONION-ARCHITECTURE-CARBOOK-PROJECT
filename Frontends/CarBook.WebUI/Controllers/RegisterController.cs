using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.IdentityServices.RegisterServices;
using CarBook.WebUI.Utilities.Settings;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IValidator<RegisterDto> _registerValidator;

        public RegisterController(IRegisterService registerService, IValidator<RegisterDto> registerValidator)
        {
            _registerService = registerService;
            _registerValidator = registerValidator;
        }

        public IActionResult Index()
        {
            ViewBag.PageTitle = "Yeni Kullanıcı Kaydı";

            AuthUIViewModel authUIViewModel = new AuthUIViewModel();

            return View(authUIViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuthUIViewModel authUIViewModel)
        {
            ViewBag.PageTitle = "Yeni Kullanıcı Kaydı";

            ValidationResult validator = _registerValidator.Validate(authUIViewModel.UserRegister);

            if (validator.IsValid)
            {
                ApiResponseSettings userRegisterResult = await _registerService.Register(authUIViewModel.UserRegister);

                if (userRegisterResult.ResponseState)
                {
                    return RedirectToAction("Index", "Login");
                }

                authUIViewModel.ResponseState = userRegisterResult.ResponseState;
                authUIViewModel.ResponseMessage = userRegisterResult.ResponseMessage;

                ModelState.AddModelError(string.Empty, userRegisterResult.ResponseMessage);
            }
            else
            {
                foreach (var item in validator.Errors)
                {
                    ModelState.AddModelError($"UserRegister.{item.PropertyName}", item.ErrorMessage);
                }
            }

            return View(authUIViewModel);

        }
    }
}
