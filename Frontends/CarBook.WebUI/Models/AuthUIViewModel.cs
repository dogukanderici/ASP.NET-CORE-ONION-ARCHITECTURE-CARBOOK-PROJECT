using CarBook.Dto.IdentityDtos;

namespace CarBook.WebUI.Models
{
    public class AuthUIViewModel
    {
        public LoginDto UserLogin { get; set; }
        public RegisterDto UserRegister { get; set; }
    }
}
