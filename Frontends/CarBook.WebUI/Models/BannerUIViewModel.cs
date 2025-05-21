using CarBook.Dto.BannerDtos;

namespace CarBook.WebUI.Models
{
    public class BannerUIViewModel
    {
        public BannerUIViewModel()
        {
            BannerDatas = new List<ResultBannerDto>();
        }

        public List<ResultBannerDto> BannerDatas { get; set; }
    }
}
