using CarBook.WebUI.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace CarBook.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public ContactController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.PageRouteTitle = "İletişim";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactUIViewModel contactUIViewModel)
        {
            contactUIViewModel.CreateData.ContactID = Guid.NewGuid();
            contactUIViewModel.CreateData.MessageType = true;
            contactUIViewModel.CreateData.ReplyID = contactUIViewModel.CreateData.ContactID;
            contactUIViewModel.CreateData.SendDate = DateTime.Now;

            var jsonData = JsonConvert.SerializeObject(contactUIViewModel.CreateData);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsync($"{_apiSettings.ApiBaseUrl}/contacts", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }

            return View(contactUIViewModel);
        }
    }
}
