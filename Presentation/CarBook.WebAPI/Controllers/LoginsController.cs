using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using CarBook.Persistance.Services.TokenServices;
using CarBook.WebAPI.Utilities.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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

            WebApiResponseSetting apiResponse = new WebApiResponseSetting()
            {
                ResponseState = loginResponse.Status,
                ResponseMessage = loginResponse.Message
            };

            return BadRequest(apiResponse);
        }
    }
}
