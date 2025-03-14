using CarBook.Dto.AuthorDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailAuthorDetailComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string blogAuthorData)
        {
            ResultAuthorDto resultAuthorDto = new ResultAuthorDto();

            if (!String.IsNullOrEmpty(blogAuthorData))
            {
                resultAuthorDto = JsonConvert.DeserializeObject<ResultAuthorDto>(blogAuthorData);
            }

            return View(resultAuthorDto);
        }
    }
}
