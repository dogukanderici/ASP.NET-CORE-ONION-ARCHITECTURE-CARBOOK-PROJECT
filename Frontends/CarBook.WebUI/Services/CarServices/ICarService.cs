using CarBook.Dto.CarDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.CarServices
{
    public interface ICarService
    {
        Task<UIServiceApiResponseSetting<ResultCarDto>> GetCarsAsync();
        Task<UIServiceApiResponseSetting<ResultCarDto>> GetCarByIdAsync(int id);
        Task<UIServiceApiResponseSetting<ResultCarDto>> GetLast5CarsAsync();
        Task<UIServiceApiResponseSetting<ResultCarDto>> GetCarForOnlyWithPricing();
        Task<HttpResponseMessage> CreateCarService(CreateCarDto updateCarDto);
        Task<HttpResponseMessage> UpdateCarService(UpdateCarDto updateCarDto);
        Task<HttpResponseMessage> DeleteCarService(int id);
    }
}
