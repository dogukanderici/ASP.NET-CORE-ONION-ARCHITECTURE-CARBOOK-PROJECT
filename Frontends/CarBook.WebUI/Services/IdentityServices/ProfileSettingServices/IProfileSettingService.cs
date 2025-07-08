using CarBook.WebUI.Areas.User.Models;

namespace CarBook.WebUI.Services.IdentityServices.ProfileSettingServices
{
    public interface IProfileSettingService
    {
        Task<bool> ChangePassword(ProfileSettingUIModel profileSettingUIModel);
    }
}
