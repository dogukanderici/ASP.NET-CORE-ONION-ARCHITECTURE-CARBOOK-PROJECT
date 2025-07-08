using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.IdentityServices.LoginServices
{
    public interface ILoginService
    {
        Task<ApiResponseSettings> Login(LoginDto loginDto);
    }
}
