using CarBook.Dto.CarPricingDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.CarPricingServices
{
    public interface ICarPricingService
    {
        Task<UIServiceApiResponseSetting<ResultCarPricingForCarDto>> GetCarPricingAsync(int id);
        Task<UIServiceApiResponseSetting<ResultCarPricingDto>> GetCarPricingByIdAsync(int id);
        Task<HttpResponseMessage> CreateCarPricingAsync(CreateCarPricingDto createCarPricingDto);
        Task<HttpResponseMessage> UpdateCarPricingAsync(UpdateCarPricingDto updateCarPricingDto);
        Task<HttpResponseMessage> DeleteCarPricingAsync(int id);
    }
}
