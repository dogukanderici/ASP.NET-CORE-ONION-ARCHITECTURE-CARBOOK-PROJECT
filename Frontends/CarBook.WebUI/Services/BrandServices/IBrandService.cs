using CarBook.Dto.BrandDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.BrandServices
{
    public interface IBrandService
    {
        Task<UIServiceApiResponseSetting<ResultBrandDto>> GetBrandAsync();
        Task<UIServiceApiResponseSetting<ResultBrandDto>> GetBrandByIdAsync(int id);
        Task<HttpResponseMessage> CreateBrandAsync(CreateBrandDto createBrandDto);
        Task<HttpResponseMessage> UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        Task<HttpResponseMessage> DeleteBrandAsync(int id);
    }
}
