using CarBook.Application.Dtos.IdentityServerDtos;
using CarBook.Application.Interfaces.TokenInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("GetToken")]
        public async Task<IActionResult> GetClientCredentialToken()
        {
            TokenResponseDto tokenResponseDto = await _tokenService.SignInWithClientCredentials();

            if (tokenResponseDto.Status)
            {
                return Ok(tokenResponseDto);
            }

            return BadRequest(tokenResponseDto);
        }
    }
}
