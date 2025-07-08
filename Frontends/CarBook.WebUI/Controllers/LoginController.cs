using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.IdentityServices.LoginServices;
using CarBook.WebUI.Utilities.Settings;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CarBook.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IValidator<LoginDto> _loginValidator;

        public LoginController(ILoginService loginService, IHttpContextAccessor contextAccessor, IValidator<LoginDto> loginValidator)
        {
            _loginService = loginService;
            _contextAccessor = contextAccessor;
            _loginValidator = loginValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Kullanıcı Girişi";

            AuthUIViewModel authUIViewModel = new AuthUIViewModel();

            return View(authUIViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuthUIViewModel authUIViewModel)
        {
            ValidationResult validator = _loginValidator.Validate(authUIViewModel.UserLogin);

            if (validator.IsValid)
            {

                ApiResponseSettings loginResponse = await _loginService.Login(authUIViewModel.UserLogin);

                if (loginResponse.ResponseState)
                {
                    // Kullanıcı Rolüne Göre Kullanıcı Sayfası veya Admin Sayfasına Yönlendirme Yapan Controller.
                    return RedirectToAction("Index", "RedirectAfterLogin");
                }

                authUIViewModel.ResponseState = loginResponse.ResponseState;
                authUIViewModel.ResponseMessage = loginResponse.ResponseMessage;
            }

            foreach (var item in validator.Errors)
            {
                ModelState.AddModelError($"UserLogin.{item.PropertyName}", item.ErrorMessage);
            }

            ViewBag.ModelStateIsValid = validator.IsValid;

            return View(authUIViewModel);
        }
    }
}
