using CarBook.Dto.CarDtos;
using CarBook.Dto.ReservationDtos;

namespace CarBook.WebUI.Services.ReservationServices
{
    public interface IReservationService
    {
        Task<HttpResponseMessage> CreateReservationForUI(CreateReservationDto createReservationDto);
        Task<List<ResultReservationDto>> GetRerservationByEmail(string email);
    }
}
