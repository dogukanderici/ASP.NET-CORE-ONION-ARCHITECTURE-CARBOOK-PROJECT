using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using CarBook.Persistance.Services.TokenServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public LoginsController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            TokenResponseDto loginResponse = await _tokenService.SignInWithResourceOwnerPassword(loginDto);

            if (loginResponse.Status)
            {
                return Ok(loginResponse);
            }

            return BadRequest(loginResponse);
        }
    }
}
