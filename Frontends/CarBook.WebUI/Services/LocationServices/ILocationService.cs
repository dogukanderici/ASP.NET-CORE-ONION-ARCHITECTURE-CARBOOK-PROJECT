using CarBook.Dto.LocationDtos;
using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.LocationServices
{
    public interface ILocationService
    {
        Task<UIServiceApiResponseSetting<ResultLocationDto>> GetLocationAsync();
        Task<UIServiceApiResponseSetting<ResultLocationDto>> GetLocationByIdAsync(int id);
        Task<HttpResponseMessage> CreateLocationAsync(CreateLocationDto createLocationDto);
        Task<HttpResponseMessage> UpdateLocationAsync(UpdateLocationDto updateLocationDto);
        Task<HttpResponseMessage> DeleteLocationAsync(int id);
    }
}
