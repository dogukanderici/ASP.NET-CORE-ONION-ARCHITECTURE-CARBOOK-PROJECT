using CarBook.WebUI.Models;
using CarBook.WebUI.Services.ContactService;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace CarBook.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
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


            HttpResponseMessage responseMessage = await _contactService.AddNewContactMessageForUI(contactUIViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }

            return View(contactUIViewModel);
        }
    }
}
