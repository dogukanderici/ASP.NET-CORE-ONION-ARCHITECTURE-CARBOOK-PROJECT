using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public RegistersController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            bool response = await _tokenService.SignUp(registerDto);

            if (response)
            {
                return Ok("Registration Success!");
            }

            return BadRequest("Registration Failed!");

        }
    }
}
