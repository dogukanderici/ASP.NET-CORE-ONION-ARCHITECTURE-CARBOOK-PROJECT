using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBook.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileSettingsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileSettingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("ChangePasswordForMember")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(changePasswordDto.Username);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.Password, changePasswordDto.NewPassword);

                if (result.Succeeded)
                {
                    return Ok("Şifre Değişikliği Başarılı!");
                }

                return BadRequest(new { status = false, messages = result.Errors.Select(e => e.Description).ToList() });

            }

            return BadRequest(new { status = false, messages = "Şifre Değişikliği Yapılacak Kullanıcı Bulunamadı!" });

        }
    }
}
