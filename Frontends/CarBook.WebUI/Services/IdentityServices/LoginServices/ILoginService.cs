using CarBook.Dto.IdentityDtos;

namespace CarBook.WebUI.Services.IdentityServices.LoginServices
{
    public interface ILoginService
    {
        Task<bool> Login(LoginDto loginDto);
    }
}
