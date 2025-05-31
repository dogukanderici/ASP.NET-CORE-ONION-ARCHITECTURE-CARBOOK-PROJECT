using CarBook.Dto.IdentityDtos;

namespace CarBook.WebUI.Services.IdentityServices.RegisterServices
{
    public interface IRegisterService
    {
        Task<bool> Register(RegisterDto registerDto);
    }
}
