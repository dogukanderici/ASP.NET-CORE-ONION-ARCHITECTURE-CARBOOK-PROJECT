using CarBook.Dto.ReservationDtos;

namespace CarBook.WebUI.Models
{
    public class ReservationUIViewModel
    {
        public ReservationUIViewModel()
        {
            CreateData = new CreateReservationDto();
        }

        public CreateReservationDto CreateData { get; set; }
    }
}
