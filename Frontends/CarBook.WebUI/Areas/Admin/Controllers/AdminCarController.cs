using CarBook.Dto.BrandDtos;
using CarBook.Dto.CarDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Services.BrandServices;
using CarBook.WebUI.Services.CarServices;
using CarBook.WebUI.Utilities.FileOperations;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;
using System.Text;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Car")]
    public class AdminCarController : AdminBaseController
    {
        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly IFileOperationHelper _fileOperationHelper;

        public AdminCarController(ICarService carService, IBrandService brandService, IFileOperationHelper fileOperationHelper)
        {
            _carService = carService;
            _brandService = brandService;
            _fileOperationHelper = fileOperationHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //UIServiceApiResponseSetting<ResultCarDto> serviceResponse = await _carService.GetCarsAsync();

            AdminUICarViewModel model = new AdminUICarViewModel();
            model.CarDatas = new List<ResultCarDto>();

            //if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            //{
            //    model.CarDatas = serviceResponse.ResponseDatas;
            //}
            //else
            //{
            //    ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
            //    ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            //}

            return View(model);
        }

        [HttpPost("PagingIndex")]
        public async Task<IActionResult> PagingIndex()
        {
            // DataTables parametreleri alınıyor
            string draw = Request.Form["draw"].FirstOrDefault();
            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            int length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());

            UIServiceApiResponseSetting<ResultCarDto> serviceResponse = await _carService.GetCarsAsync(start, length);

            AdminUICarViewModel model = new AdminUICarViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.CarDatas = serviceResponse.ResponseDatas;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            var dtResponse = new
            {
                draw = draw,
                recordsTotal = serviceResponse.TotalDataCount,
                recordsFiltered = serviceResponse.TotalDataCount,
                data = model.CarDatas
            };

            return Json(dtResponse);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> CreateCar()
        {
            List<SelectListItem> brandList = await CarBrands();
            List<SelectListItem> transmissionList = GetTransmissionType();
            List<SelectListItem> fuelTypeList = GetFuelType();

            ViewBag.BrandList = brandList;
            ViewBag.TransmissionList = transmissionList;
            ViewBag.FuelTypeList = fuelTypeList;

            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCar(AdminUICarViewModel adminUICarViewModel)
        {
            string coverImageUrlString = await _fileOperationHelper.CopyFileToFolder(new FileProperty
            {
                FilePath = "/wwwroot/assets/car_photos/",
                LoadedFile = adminUICarViewModel.CreateCarData.CoverImage
            });

            adminUICarViewModel.CreateCarData.CoverImageURL = coverImageUrlString;

            string bigImageUrlString = await _fileOperationHelper.CopyFileToFolder(new FileProperty
            {
                FilePath = "/wwwroot/assets/car_photos/",
                LoadedFile = adminUICarViewModel.CreateCarData.BigImage
            });

            adminUICarViewModel.CreateCarData.BigImageURL = bigImageUrlString;

            HttpResponseMessage serviceResponse = await _carService.CreateCarService(adminUICarViewModel.CreateCarData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUICarViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateCar(int id)
        {
            List<SelectListItem> brandList = await CarBrands();
            List<SelectListItem> transmissionList = GetTransmissionType();
            List<SelectListItem> fuelTypeList = GetFuelType();

            ViewBag.BrandList = brandList;
            ViewBag.TransmissionList = transmissionList;
            ViewBag.FuelTypeList = fuelTypeList;

            UIServiceApiResponseSetting<ResultCarDto> serviceResponse = await _carService.GetCarByIdAsync(id);

            AdminUICarViewModel model = new AdminUICarViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCarDto>(jsonData);

                model.UpdateCarData = value;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCar(AdminUICarViewModel adminUICarViewModel)
        {
            if (adminUICarViewModel.UpdateCarData.CoverImage != null)
            {
                string coverImageUrlString = await _fileOperationHelper.CopyFileToFolder(new FileProperty
                {
                    FilePath = "/wwwroot/assets/car_photos/",
                    LoadedFile = adminUICarViewModel.UpdateCarData.CoverImage
                });

                adminUICarViewModel.UpdateCarData.CoverImageURL = coverImageUrlString;
            }

            if (adminUICarViewModel.UpdateCarData.BigImage != null)
            {
                string bigImageUrlString = await _fileOperationHelper.CopyFileToFolder(new FileProperty
                {
                    FilePath = "/wwwroot/assets/car_photos/",
                    LoadedFile = adminUICarViewModel.UpdateCarData.BigImage
                });

                adminUICarViewModel.UpdateCarData.BigImageURL = bigImageUrlString;
            }

            HttpResponseMessage serviceResponse = await _carService.UpdateCarService(adminUICarViewModel.UpdateCarData);

            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUICarViewModel.UpdateCarData);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            HttpResponseMessage serviceResponse = await _carService.DeleteCarService(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
        }

        private async Task<List<SelectListItem>> CarBrands()
        {
            UIServiceApiResponseSetting<ResultBrandDto> serviceResponse = await _brandService.GetBrandAsync();

            List<SelectListItem> brandList = new List<SelectListItem>();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                List<ResultBrandDto> values = serviceResponse.ResponseDatas;

                brandList = (from item in values
                             select new SelectListItem
                             {
                                 Text = item.BrandName,
                                 Value = item.BrandID.ToString()
                             }).ToList();
            }

            return brandList;
        }

        private List<SelectListItem> GetFuelType()
        {
            List<SelectListItem> fuelTypeList = new List<SelectListItem>();


            fuelTypeList.Add(new SelectListItem
            {
                Text = "Benzin",
                Value = "Benzin"
            });

            fuelTypeList.Add(new SelectListItem
            {
                Text = "Dizel",
                Value = "Dizel"
            });

            fuelTypeList.Add(new SelectListItem
            {
                Text = "Hibrit",
                Value = "Hibrit"
            });

            fuelTypeList.Add(new SelectListItem
            {
                Text = "Elektrik",
                Value = "Elektrik"
            });

            return fuelTypeList;
        }

        private List<SelectListItem> GetTransmissionType()
        {
            List<SelectListItem> transmissionList = new List<SelectListItem>();

            transmissionList.Add(new SelectListItem
            {
                Text = "Manuel",
                Value = "Manuel"
            });

            transmissionList.Add(new SelectListItem
            {
                Text = "Otomatik",
                Value = "Otomatik"
            });

            return transmissionList;
        }
    }
}
