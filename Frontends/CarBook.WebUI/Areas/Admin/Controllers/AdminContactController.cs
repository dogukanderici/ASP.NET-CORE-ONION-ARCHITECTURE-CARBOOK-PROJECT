using CarBook.Dto.ContactDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.ContactService;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class AdminContactController : AdminBaseController
    {
        private readonly IContactService _contactService;

        public AdminContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Inbox()
        {

            UIServiceApiResponseSetting<ResultContactDto> serviceResponse = await _contactService.GetContactInboxAsync();

            AdminUIContactViewModel model = new AdminUIContactViewModel();

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

        [HttpGet("Outbox")]
        public async Task<IActionResult> Outbox()
        {
            UIServiceApiResponseSetting<ResultContactDto> serviceResponse = await _contactService.GetContactOutboxAsync();

            AdminUIContactViewModel model = new AdminUIContactViewModel();

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

        [HttpGet("OutboxDetail")]
        public async Task<IActionResult> OutboxDetail(Guid id)
        {
            UIServiceApiResponseSetting<ResultContactDto> serviceResponse = await _contactService.GetContactByIdAsync(id);

            AdminUIContactViewModel model = new AdminUIContactViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultData = serviceResponse.ResponseData;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpGet("Reply")]
        public async Task<IActionResult> ReplyContact(Guid id)
        {
            UIServiceApiResponseSetting<ResultContactDto> serviceResponse = await _contactService.GetContactByIdAsync(id);

            AdminUIContactViewModel model = new AdminUIContactViewModel();

            if (serviceResponse.HttpResponseMessage.IsSuccessStatusCode)
            {
                model.ResultData = serviceResponse.ResponseData;
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.HttpResponseMessage.StatusCode;
                ViewBag.ErrorMessage = await serviceResponse.HttpResponseMessage.Content.ReadAsStringAsync();
            }

            return View(model);
        }

        [HttpPost("Reply")]
        public async Task<IActionResult> ReplyContact(AdminUIContactViewModel adminUIContactViewModel)
        {
            adminUIContactViewModel.CreateData.ContactID = Guid.NewGuid();
            adminUIContactViewModel.CreateData.MessageType = false;
            adminUIContactViewModel.CreateData.ReplyID = adminUIContactViewModel.CreateData.ReplyID;
            adminUIContactViewModel.CreateData.SendDate = DateTime.Now;
            adminUIContactViewModel.CreateData.Name = "CarBook Admin";
            adminUIContactViewModel.CreateData.Email = "support@carbook.com";

            HttpResponseMessage serviceResponse = await _contactService.AddNewContactMessageForUI(adminUIContactViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "AdminContact", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUIContactViewModel);
        }

        [HttpGet("Create")]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateContact(AdminUIContactViewModel adminUIContactViewModel)
        {
            HttpResponseMessage serviceResponse = await _contactService.AddNewContactMessageForUI(adminUIContactViewModel.CreateData);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (serviceResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminContact", new { area = "Admin" });
            }
            else
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return View(adminUIContactViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            HttpResponseMessage serviceResponse = await _contactService.DeleteContactAsync(id);
            string apiMessage = await serviceResponse.Content.ReadAsStringAsync();

            if (!serviceResponse.IsSuccessStatusCode)
            {
                ViewBag.ErrorCode = serviceResponse.StatusCode;
                ViewBag.ErrorMessage = apiMessage;
            }

            return RedirectToAction("Index", "AdminContact", new { area = "Admin" });
        }
    }
}
