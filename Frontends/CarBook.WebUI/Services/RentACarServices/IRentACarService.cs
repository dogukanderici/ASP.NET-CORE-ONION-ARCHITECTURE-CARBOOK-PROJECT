using CarBook.Dto.RentACarDtos;
using System.Collections.Specialized;

namespace CarBook.WebUI.Services.RentACarServices
{
    public interface IRentACarService
    {
        Task<List<ResultRentACarDto>> GetRentACarWithAvailablity(NameValueCollection query);
    }
}
