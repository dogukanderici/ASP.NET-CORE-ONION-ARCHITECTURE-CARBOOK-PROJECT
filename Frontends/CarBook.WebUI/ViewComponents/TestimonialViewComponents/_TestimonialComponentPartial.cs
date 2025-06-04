using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Models;
using CarBook.WebUI.Services.TestimonialServices;
using CarBook.WebUI.Utilities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarBook.WebUI.ViewComponents.TestimonialViewComponents
{
    public class _TestimonialComponentPartial : ViewComponent
    {
        private readonly ITestimonialService _testimonialService;

        public _TestimonialComponentPartial(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<ResultTestimonialDto> values = await _testimonialService.GetTestimonialsAsync();

            TestimonailUIViewModel model = new TestimonailUIViewModel();

            model.TestimonialDatas = values;

            return View(model);
        }
    }
}
