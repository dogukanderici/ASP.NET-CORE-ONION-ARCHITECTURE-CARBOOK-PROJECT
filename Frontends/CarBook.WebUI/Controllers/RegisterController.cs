using CarBook.Dto.IdentityDtos;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            await _registerService.Register(registerDto);

            return RedirectToAction("Index", "Login");
        }
    }
}
