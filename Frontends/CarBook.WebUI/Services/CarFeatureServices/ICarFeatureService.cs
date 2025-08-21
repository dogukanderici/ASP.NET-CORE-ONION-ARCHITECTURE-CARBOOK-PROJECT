using CarBook.Dto.CarFeatureDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.CarFeatureServices
{
    public interface ICarFeatureService
    {
        Task<UIServiceApiResponseSetting<ResultCarFeatureDto>> GetCarFeatureAsync();
        Task<UIServiceApiResponseSetting<ResultCarFeatureDto>> GetCarFeatureByIdAsync(int id);
        Task<HttpResponseMessage> CreateCarFeatureAsync(List<CreateCarFeatureDto> createCarFeatureDtos);
        Task<HttpResponseMessage> UpdateCarFeatureAsync(UpdateCarFeatureDto updateCarFeatureDto);
        Task<HttpResponseMessage> DeleteCarFeatureAsync(int id);
    }
}
