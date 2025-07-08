using CarBook.Dto.AuthorDtos;
using CarBook.WebUI.Areas.Admin.Models;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Author")]
    public class AdminAuthorController : AdminBaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminAuthorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync("authors");

            AdminUIAuthorViewModel model = new AdminUIAuthorViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultAuthorDto>>(jsonData);

                model.ResultDatas = value;
            }

            return View(model);
        }

        [HttpGet("Create")]
        public IActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAuthor(AdminUIAuthorViewModel adminUIAuthorViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PostAsJsonAsync<CreateAuthorDto>("authors", adminUIAuthorViewModel.CreateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
            }

            return View(adminUIAuthorViewModel);
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateAuthor(Guid id)
        {
            var client = _httpClientFactory.CreateClient("ReadOnlyClient");
            var responseMessage = await client.GetAsync($"authors/{id}");

            AdminUIAuthorViewModel model = new AdminUIAuthorViewModel();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAuthorDto>(jsonData);

                model.UpdateData = value;
            }

            return View(model);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAuthor(AdminUIAuthorViewModel adminUIAuthorViewModel)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.PutAsJsonAsync<UpdateAuthorDto>("authors", adminUIAuthorViewModel.UpdateData);

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();

                return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
            }

            return View(adminUIAuthorViewModel);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var client = _httpClientFactory.CreateClient("FullAuthClient");
            var responseMessage = await client.DeleteAsync($"authors?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var apiMessage = await responseMessage.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
        }
    }
}
