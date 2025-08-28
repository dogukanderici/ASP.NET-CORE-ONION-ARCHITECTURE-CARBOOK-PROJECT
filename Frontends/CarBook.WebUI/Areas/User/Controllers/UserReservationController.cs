using CarBook.Dto.CarDtos;
using CarBook.Dto.ReservationDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.ReservationServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace CarBook.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Reservation")]
    public class UserReservationController : UserBaseController
    {
        private readonly IReservationService _reservationService;

        public UserReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("History")]
        public async Task<IActionResult> History()
        {
            ViewBag.PageTitle = "Araç Kiralama";
            ViewBag.MainPageTitle = "Araç Kirala";
            ViewBag.SubPageTitle = "Geçmiş Kiralamalar";

            ReservationUIViewModel model = new ReservationUIViewModel();

            if (User.Identity.IsAuthenticated)
            {
                string userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
                List<ResultReservationDto> result = await _reservationService.GetRerservationByEmail(userEmail);

                model.ResultDatas = result;

            }

            return View(model);
        }

        [HttpGet("HistoryDetail")]
        public async Task<IActionResult> HistoryDetail(Guid id)
        {
            ViewBag.PageTitle = "Araç Kiralama";
            ViewBag.MainPageTitle = "Geçmiş Kiralamalar";
            ViewBag.SubPageTitle = "Kiralama Detayı";

            ReservationUIViewModel model = new ReservationUIViewModel();

            if (User.Identity.IsAuthenticated)
            {
                ResultReservationDto result = await _reservationService.GetReservationById(id);

                model.ResultData = result;
            }

            return View(model);
        }
    }
}
