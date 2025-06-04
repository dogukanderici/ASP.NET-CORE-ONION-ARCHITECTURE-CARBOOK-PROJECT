using CarBook.Dto.ServiceDtos;

namespace CarBook.WebUI.Services.OurServiceServices
{
    public interface IOurServiceService
    {
        Task<List<ResultServiceDto>> GetServicesAsync();
    }
}
