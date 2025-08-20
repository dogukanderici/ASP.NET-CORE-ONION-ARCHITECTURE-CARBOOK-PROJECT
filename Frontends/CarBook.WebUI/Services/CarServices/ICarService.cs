using CarBook.Dto.CarDtos;

namespace CarBook.WebUI.Services.CarServices
{
    public interface ICarService
    {
        Task<List<ResultCarDto>> GetLast5CarsAsync();
        Task<List<ResultCarDto>> GetCarsAsync();
        Task<ResultCarDto> GetCarByIdAsync(int id);
        Task<List<ResultCarDto>> GetCarForOnlyWithPricing();
        Task UpdateCarService(UpdateCarDto dto);
    }
}
