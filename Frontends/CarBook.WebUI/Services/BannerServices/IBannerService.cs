using CarBook.Dto.BannerDtos;

namespace CarBook.WebUI.Services.BannerServices
{
    public interface IBannerService
    {
        Task<List<ResultBannerDto>> GetBannerAsync();
    }
}
