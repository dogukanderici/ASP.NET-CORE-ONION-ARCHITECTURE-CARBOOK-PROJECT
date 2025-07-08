using CarBook.Dto.ReservationDtos;

namespace CarBook.WebUI.Models
{
    public class ReservationUIViewModel
    {
        public ReservationUIViewModel()
        {
            CreateData = new CreateReservationDto();
            ResultDatas = new List<ResultReservationDto>();
        }

        public CreateReservationDto CreateData { get; set; }
        public List<ResultReservationDto> ResultDatas { get; set; }
    }
}
