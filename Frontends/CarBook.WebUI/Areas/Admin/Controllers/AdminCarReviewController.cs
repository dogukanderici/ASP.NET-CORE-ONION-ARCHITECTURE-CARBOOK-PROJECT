using CarBook.Dto.CarReviewDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.CarReviewServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarReview")]
    public class AdminCarReviewController : AdminBaseController
    {
        private readonly ICarReviewService _carReviewService;

        public AdminCarReviewController(ICarReviewService carReviewService)
        {
            _carReviewService = carReviewService;
        }


        [HttpGet("{carId}")]
        public async Task<IActionResult> Index(int carId, bool? status)
        {
            UIServiceApiResponseSetting<ResultCarReviewDto> serviceResponse = await _carReviewService.GetCarReviewByCarIdAsync(carId, status);

            AdminUICarReviewViewModel model = new AdminUICarReviewViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
            }

            return View(model);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateCarReview(Guid id)
        {
            UIServiceApiResponseSetting<ResultCarReviewDto> serviceResponse = await _carReviewService.GetCarReviewByIdAsync(id);

            AdminUICarReviewViewModel model = new AdminUICarReviewViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateCarReviewDto value = JsonConvert.DeserializeObject<UpdateCarReviewDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCarReview(AdminUICarReviewViewModel adminUICarReviewViewModel)
        {
            HttpResponseMessage serviceResponse = await _carReviewService.UpdateCarReviewAsync(adminUICarReviewViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCarReview", new { area = "Admin", carId = adminUICarReviewViewModel.UpdateData.CarID });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUICarReviewViewModel);
        }

        [HttpGet("Delete")]
        public IActionResult DeleteCarReview(int carId)
        {
            return View();
        }
    }
}
