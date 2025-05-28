using CarBook.Dto.CarReviewDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailReviewByCarIdComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _CarDetailReviewByCarIdComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(int carId)
        {
            var client = _httpClientFactory.CreateClient();
            var requestResponse = await client.GetAsync($"{_apiSettings.ApiBaseUrl}/carreviews/carreviewwithcar/{carId}");

            CarReviewUIViewModel model = new CarReviewUIViewModel();

            if (requestResponse.IsSuccessStatusCode)
            {
                var jsonData = await requestResponse.Content.ReadFromJsonAsync<List<ResultCarReviewDto>>();
                model.CarReviews = jsonData;

                ViewBag.StarsFive = jsonData.Count(x => x.CommentStar == 5);
                ViewBag.StarsFour = jsonData.Count(x => x.CommentStar == 4);
                ViewBag.StarsThree = jsonData.Count(x => x.CommentStar == 3);
                ViewBag.StarsTwo = jsonData.Count(x => x.CommentStar == 2);
                ViewBag.StarsOne = jsonData.Count(x => x.CommentStar == 1);
            }

            return View(model);
        }
    }
}
