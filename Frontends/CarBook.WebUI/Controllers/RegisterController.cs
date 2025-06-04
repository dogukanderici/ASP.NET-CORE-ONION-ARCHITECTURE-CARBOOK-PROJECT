using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.IdentityServices.RegisterServices;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        public IActionResult Index()
        {
            ViewBag.PageTitle = "Yeni Kullanıcı Kaydı";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuthUIViewModel authUIViewModel)
        {
            await _registerService.Register(authUIViewModel.UserRegister);

            return RedirectToAction("Index", "Login");
        }
    }
}
