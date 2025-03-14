using CarBook.Dto.LocationDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUILocationViewModel
    {
        public List<ResultLocationDto> ResultDatas { get; set; }
        public CreateLocationDto CreateData { get; set; }
        public UpdateLocationDto UpdateData { get; set; }
    }
}
