using CarBook.Dto.CarReviewDtos;

namespace CarBook.WebUI.Models
{
    public class CarReviewUIViewModel
    {
        public CarReviewUIViewModel()
        {
            CarReviews = new List<ResultCarReviewDto>();
        }

        public List<ResultCarReviewDto> CarReviews { get; set; }
    }
}
