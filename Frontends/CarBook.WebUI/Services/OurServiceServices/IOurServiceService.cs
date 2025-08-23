using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.OurServiceServices
{
    public interface IOurServiceService
    {
        Task<UIServiceApiResponseSetting<ResultServiceDto>> GetServiceAsync();
        Task<UIServiceApiResponseSetting<ResultServiceDto>> GetServiceByIdAsync(int id);
        Task<HttpResponseMessage> CreateServiceAsync(CreateServiceDto createServiceDto);
        Task<HttpResponseMessage> UpdateServiceAsync(UpdateServiceDto updateServiceDto);
        Task<HttpResponseMessage> DeleteServiceAsync(int id);
    }
}
