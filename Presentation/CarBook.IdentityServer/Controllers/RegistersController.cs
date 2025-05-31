using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var values = new ApplicationUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname
            };

            var result = await _userManager.CreateAsync(values, registerDto.Password);

            if (result.Succeeded)
            {
                return Ok("Kullanıcı Başarıyla Oluşturuldu.");
            }

            return BadRequest(new { success = false, message = result.Errors.Select(e => e.Description).ToList() });
        }
    }
}
