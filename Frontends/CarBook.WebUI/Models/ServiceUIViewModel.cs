using CarBook.Dto.ServiceDtos;

namespace CarBook.WebUI.Models
{
    public class ServiceUIViewModel
    {
        public ServiceUIViewModel()
        {
            ServiceDatas = new List<ResultServiceDto>();
        }

        public List<ResultServiceDto> ServiceDatas { get; set; }
    }
}
