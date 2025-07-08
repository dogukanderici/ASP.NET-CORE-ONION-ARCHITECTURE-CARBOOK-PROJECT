using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileSettingsController : BaseController
    {
        private readonly ITokenService _tokenService;

        public ProfileSettingsController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("ChangeMemberPassword")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public async Task<IActionResult> ChangePasswordForMember(ChangePasswordDto changePasswordDto)
        {
            string user = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            changePasswordDto.Username = user;
            bool result = await _tokenService.ChangePasswordForMember(changePasswordDto);

            if (result)
            {
                return Ok("Şifre Değişikliği Başarılı!");
            }

            return BadRequest("Şifre Değişikliği Yapılırken Bir Sorun Oluştu!");
        }
    }
}
