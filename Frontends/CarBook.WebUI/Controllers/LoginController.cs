using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.IdentityServices.LoginServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;


        public LoginController(ILoginService loginService, IHttpContextAccessor contextAccessor)
        {
            _loginService = loginService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Kullanıcı Girişi";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AuthUIViewModel authUIViewModel)
        {
            // TODO - Login validasyonları yapılacak.
            await _loginService.Login(authUIViewModel.UserLogin);

            // TODO - Kullanıcı Rolüne Göre Kullanıcı Sayfası veya Admin Sayfasına Yönlendirme Yapacak Controller Yazılacak.
            return RedirectToAction("Index", "Default");
        }
    }
}
