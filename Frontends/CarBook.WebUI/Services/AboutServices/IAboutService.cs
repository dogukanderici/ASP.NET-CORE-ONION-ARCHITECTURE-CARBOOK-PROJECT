using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.AboutServices
{
    public interface IAboutService
    {
        Task<UIServiceApiResponseSetting<ResultAboutDto>> GetAboutAsync();
        Task<UIServiceApiResponseSetting<ResultAboutDto>> GetAboutByIdAsync(int id);
        Task<HttpResponseMessage> CreateAboutAsync(CreateAboutDto createAboutDto);
        Task<HttpResponseMessage> UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task<HttpResponseMessage> DeleteAboutAsync(int id);
    }
}
