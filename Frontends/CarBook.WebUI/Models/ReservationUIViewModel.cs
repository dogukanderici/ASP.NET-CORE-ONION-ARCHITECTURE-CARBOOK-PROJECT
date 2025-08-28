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

        public List<ResultReservationDto> ResultDatas { get; set; }
        public ResultReservationDto ResultData { get; set; }
        public CreateReservationDto CreateData { get; set; }
    }
}
