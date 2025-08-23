using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.TestimonialServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Testimonial")]
    public class AdminTestimonialController : AdminBaseController
    {
        private readonly ITestimonialService _testimonialService;

        public AdminTestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultTestimonialDto> serviceResponse = await _testimonialService.GetTestimonialAsync();

            AdminUITestimonialViewModel model = new AdminUITestimonialViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTestimonial(AdminUITestimonialViewModel adminUITestimonialViewModel)
        {
            HttpResponseMessage serviceResponse = await _testimonialService.CreateTestimonialAsync(adminUITestimonialViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUITestimonialViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            UIServiceApiResponseSetting<ResultTestimonialDto> serviceResponse = await _testimonialService.GetTestimonialByIdAsync(id);

            AdminUITestimonialViewModel model = new AdminUITestimonialViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateTestimonialDto value = JsonConvert.DeserializeObject<UpdateTestimonialDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateTestimonial(AdminUITestimonialViewModel adminUITestimonialViewModel)
        {
            HttpResponseMessage serviceResponse = await _testimonialService.UpdateTestimonialAsync(adminUITestimonialViewModel.UpdateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUITestimonialViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            HttpResponseMessage serviceResponse = await _testimonialService.DeleteTestimonialAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
        }
    }
}
