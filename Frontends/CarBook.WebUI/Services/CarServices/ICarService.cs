using CarBook.Dto.CarDtos;

namespace CarBook.WebUI.Services.CarServices
{
    public interface ICarService
    {
        Task<List<ResultCarDto>> GetLast5CarsAsync();
    }
}
