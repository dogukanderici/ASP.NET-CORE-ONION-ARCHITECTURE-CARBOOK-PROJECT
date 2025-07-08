using CarBook.Dto.ContactDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class AdminContactController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Inbox()
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.GetAsync("contacts/inbox");

            AdminUIContactViewModel model = new AdminUIContactViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("Outbox")]
        public async Task<IActionResult> Outbox()
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.GetAsync("contacts/outbox");

            AdminUIContactViewModel model = new AdminUIContactViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("OutboxDetail")]
        public async Task<IActionResult> OutboxDetail(Guid id)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.GetAsync($"contacts/{id}");

            AdminUIContactViewModel model = new AdminUIContactViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultContactDto>(jsonData);

                model.ResultData = value;
            }

            return View(model);
        }

        [HttpGet("Reply")]
        public async Task<IActionResult> ReplyContact(Guid id)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.GetAsync($"contacts/{id}");

            AdminUIContactViewModel model = new AdminUIContactViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultContactDto>(jsonData);

                model.ResultData = value;
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

            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<CreateContactDto>("contacts", adminUIContactViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminContact", new { area = "Admin" });
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
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<CreateContactDto>("contacts", adminUIContactViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminContact", new { area = "Admin" });
            }

            return View(adminUIContactViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.DeleteAsync($"contacts?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminContact", new { area = "Admin" });
        }
    }
}
