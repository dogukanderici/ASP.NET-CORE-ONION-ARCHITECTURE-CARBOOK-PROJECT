using CarBook.Dto.CarReviewDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUICarReviewViewModel
    {
        public List<ResultCarReviewDto> ResultDatas { get; set; }
        public List<ResultCarReviewDto> ResultData { get; set; }
        public CreateCarReviewDto CreateData { get; set; }
        public UpdateCarReviewDto UpdateData { get; set; }
    }
}
