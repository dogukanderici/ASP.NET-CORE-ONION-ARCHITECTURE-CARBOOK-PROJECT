using CarBook.Dto.SocialMediaDtos;

namespace CarBook.WebUI.Services.SocialMediaServices
{
    public interface ISocialMediaService
    {
        Task<List<ResultSocialMediaDto>> GetSocialMediaAsync();
    }
}
