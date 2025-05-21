using CarBook.Dto.CarDtos;

namespace CarBook.WebUI.Models
{
    public class CarUIViewModel
    {
        public CarUIViewModel()
        {
            CarDatas = new List<ResultCarDto>();
        }

        public List<ResultCarDto> CarDatas { get; set; }
    }
}
