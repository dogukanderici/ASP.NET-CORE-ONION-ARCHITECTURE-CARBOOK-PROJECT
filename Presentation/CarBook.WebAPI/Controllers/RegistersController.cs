using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using CarBook.WebAPI.Utilities.Settings;
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
            HttpResponseMessage response = await _tokenService.SignUp(registerDto);

            WebApiResponseSetting apiResponse = new WebApiResponseSetting()
            {
                ResponseState = response.IsSuccessStatusCode
            };

            apiResponse.ResponseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                apiResponse.ResponseState = true;

                return Ok(apiResponse);
            }

            return BadRequest(apiResponse);

        }
    }
}
