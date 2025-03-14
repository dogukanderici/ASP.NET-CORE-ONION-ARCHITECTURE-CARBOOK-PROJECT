using CarBook.Dto.CarDtos;
using CarBook.Dto.CarFeatureDtos;
using CarBook.Dto.FeatureDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUICarViewModel
    {
        public List<ResultCarDto> CarDatas { get; set; }
        public CreateCarDto CreateCarData { get; set; }
        public UpdateCarDto UpdateCarData { get; set; }
    }
}
