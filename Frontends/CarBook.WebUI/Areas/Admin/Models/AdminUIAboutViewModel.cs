using CarBook.Dto.AboutDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIAboutViewModel
    {
        public List<ResultAboutDto> ResultDatas { get; set; }
        public CreateAboutDto CreateData { get; set; }
        public UpdateAboutDto UpdateData { get; set; }
    }
}
