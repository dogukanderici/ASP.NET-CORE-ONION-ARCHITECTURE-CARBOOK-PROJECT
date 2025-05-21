using CarBook.Dto.LocationDtos;

namespace CarBook.WebUI.Models
{
    public class LocationUIViewModel
    {
        public LocationUIViewModel()
        {
            ResultDatas = new List<ResultLocationDto>();
        }

        public List<ResultLocationDto> ResultDatas { get; set; }
    }
}
