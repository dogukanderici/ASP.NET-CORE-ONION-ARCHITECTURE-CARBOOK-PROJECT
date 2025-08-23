using CarBook.Dto.CarReviewDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.CarReviewServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailReviewByCarIdComponentPartial : ViewComponent
    {
        private readonly ICarReviewService _carReviewService;

        public _CarDetailReviewByCarIdComponentPartial(ICarReviewService carReviewService)
        {
            _carReviewService = carReviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int carId)
        {
            UIServiceApiResponseSetting<ResultCarReviewDto> serviceResponse = await _carReviewService.GetCarReviewByCarIdAsync(carId, true);

            CarReviewUIViewModel model = new CarReviewUIViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarReviews = serviceResponse.ResponseDatas;

                ViewBag.StarsFive = serviceResponse.ResponseDatas.Count(x => x.CommentStar == 5);
                ViewBag.StarsFour = serviceResponse.ResponseDatas.Count(x => x.CommentStar == 4);
                ViewBag.StarsThree = serviceResponse.ResponseDatas.Count(x => x.CommentStar == 3);
                ViewBag.StarsTwo = serviceResponse.ResponseDatas.Count(x => x.CommentStar == 2);
                ViewBag.StarsOne = serviceResponse.ResponseDatas.Count(x => x.CommentStar == 1);
            }

            return View(model);
        }
    }
}
