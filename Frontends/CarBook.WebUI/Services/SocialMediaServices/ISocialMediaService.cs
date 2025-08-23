using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.SocialMediaServices
{
    public interface ISocialMediaService
    {
        Task<UIServiceApiResponseSetting<ResultSocialMediaDto>> GetSocialMediaAsync();
        Task<UIServiceApiResponseSetting<ResultSocialMediaDto>> GetSocialMediaByIdAsync(int id);
        Task<HttpResponseMessage> CreateSocialMediaAsync(CreateSocialMediaDto createSocialMediaDto);
        Task<HttpResponseMessage> UpdateSocialMediaAsync(UpdateSocialMediaDto updateSocialMediaDto);
        Task<HttpResponseMessage> DeleteSocialMediaAsync(int id);
    }
}
