using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<UIServiceApiResponseSetting<ResultFeatureDto>> GetFeatureAsync();
        Task<UIServiceApiResponseSetting<ResultFeatureDto>> GetFeatureByIdAsync(int id);
        Task<HttpResponseMessage> CreateFeatureAsync(CreateFeatureDto createFeatureDtos);
        Task<HttpResponseMessage> UpdateFeatureAsync(UpdateFeatureDto updateFeatureDtos);
        Task<HttpResponseMessage> DeleteFeatureAsync(int id);
    }
}
