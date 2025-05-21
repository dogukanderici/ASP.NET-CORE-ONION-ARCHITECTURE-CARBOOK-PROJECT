using CarBook.Dto.AboutDtos;

namespace CarBook.WebUI.Models
{
    public class AboutUIViewModel
    {
        public AboutUIViewModel()
        {
            AboutDatas = new List<ResultAboutDto>();
        }

        public List<ResultAboutDto> AboutDatas { get; set; }
    }
}
