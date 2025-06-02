using CarBook.Dto.LocationDtos;

namespace CarBook.WebUI.Services.LocationServices
{
    public interface ILocationService
    {
        Task<List<ResultLocationDto>> GetLocationsAsync();
    }
}
