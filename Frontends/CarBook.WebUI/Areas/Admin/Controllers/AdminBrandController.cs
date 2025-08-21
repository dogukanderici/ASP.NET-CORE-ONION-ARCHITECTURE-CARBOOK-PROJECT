using CarBook.Dto.BrandDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.BrandServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class AdminBrandController : AdminBaseController
    {
        private readonly IBrandService _brandService;

        public AdminBrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UIServiceApiResponseSetting<ResultBrandDto> serviceResponse = await _brandService.GetBrandAsync();

            AdminUIBrandViewModel model = new AdminUIBrandViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }


        [HttpGet("Create")]
        public IActionResult CreateBrand()
        {
            AdminUIBrandViewModel model = new AdminUIBrandViewModel();

            return View(model);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateBrand(AdminUIBrandViewModel adminUIBrandViewModel)
        {
            HttpResponseMessage serviceResponse = await _brandService.CreateBrandAsync(adminUIBrandViewModel.CreateData);

            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBrand", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.Content;
            }

            return View(adminUIBrandViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            UIServiceApiResponseSetting<ResultBrandDto> serviceResponse = await _brandService.GetBrandByIdAsync(id);

            AdminUIBrandViewModel model = new AdminUIBrandViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                string jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                UpdateBrandDto value = JsonConvert.DeserializeObject<UpdateBrandDto>(jsonData);

                model.UpdateData = value;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.HttpResponseMessage.Content;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateBrand(AdminUIBrandViewModel adminUIBrandViewModel)
        {
            HttpResponseMessage serviceResponse = await _brandService.UpdateBrandAsync(adminUIBrandViewModel.UpdateData);
            string apiResponse = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "AdminBrand", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.Content;
            }

            return View(adminUIBrandViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            HttpResponseMessage serviceResponse = await _brandService.DeleteBrandAsync(id);

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = serviceResponse.Content;
            }

            return RedirectToAction("Index", "AdminBrand", new { area = "Admin" });
        }
    }
}
