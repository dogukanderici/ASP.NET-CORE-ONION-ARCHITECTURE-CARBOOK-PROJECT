using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Models
{
    public class AuthUIViewModel : ApiResponseSettings
    {
        public AuthUIViewModel()
        {
            ResponseState = true;
        }

        public LoginDto UserLogin { get; set; }
        public RegisterDto UserRegister { get; set; }
    }
}
