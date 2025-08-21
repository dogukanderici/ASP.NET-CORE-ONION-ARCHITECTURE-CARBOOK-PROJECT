using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.BannerServices
{
    public interface IBannerService
    {
        Task<UIServiceApiResponseSetting<ResultBannerDto>> GetBannerAsync();
        Task<UIServiceApiResponseSetting<ResultBannerDto>> GetBannerByIdAsync(int id);
        Task<HttpResponseMessage> CreateBannerAsync(CreateBannerDto createBannerDto);
        Task<HttpResponseMessage> UpdateBannerAsync(UpdateBannerDto updateBannerDto);
        Task<HttpResponseMessage> DeleteBannerAsync(int id);
    }
}
