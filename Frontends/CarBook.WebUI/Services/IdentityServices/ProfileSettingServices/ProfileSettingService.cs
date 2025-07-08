using CarBook.Dto.IdentityDtos;
using CarBook.WebUI.Areas.User.Models;
using System.Net.Http.Json;

namespace CarBook.WebUI.Services.IdentityServices.ProfileSettingServices
{

    public class ProfileSettingService : IProfileSettingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileSettingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> ChangePassword(ProfileSettingUIModel profileSettingUIModel)
        {
            HttpClient client = _httpClientFactory.CreateClient("FullAuthClient");

            HttpResponseMessage response = await client.PostAsJsonAsync<ChangePasswordDto>("profilesettings/changememberpassword", profileSettingUIModel.ChangePassword);

            if (response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();

                return true;
            }

            return false;
        }
    }
}
