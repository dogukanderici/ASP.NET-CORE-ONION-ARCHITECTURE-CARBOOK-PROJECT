using CarBook.Dto.ServiceDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIServiceViewModel
    {
        public List<ResultServiceDto> ResultDatas { get; set; }
        public CreateServiceDto CreateData { get; set; }
        public UpdateServiceDto UpdateData { get; set; }
    }
}
