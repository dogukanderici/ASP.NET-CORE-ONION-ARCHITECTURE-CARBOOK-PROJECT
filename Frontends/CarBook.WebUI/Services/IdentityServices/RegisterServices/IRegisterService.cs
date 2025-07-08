using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.IdentityServices.RegisterServices
{
    public interface IRegisterService
    {
        Task<ApiResponseSettings> Register(RegisterDto registerDto);
    }
}
