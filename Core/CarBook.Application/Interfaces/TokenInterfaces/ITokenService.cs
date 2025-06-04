using CarBook.Application.Dtos.IdentityServerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Interfaces.TokenInterfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDto> SignInWithResourceOwnerPassword(LoginDto loginDto);
        Task<TokenResponseDto> SignInWithClientCredentials();
        Task<TokenResponseDto> RefreshToken();
        Task<bool> SignUp(RegisterDto registerDto);
    }
}
