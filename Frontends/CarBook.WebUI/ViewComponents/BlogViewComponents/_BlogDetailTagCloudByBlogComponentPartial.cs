using CarBook.Dto.BlogTagCloudDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarBook.WebUI.ViewComponents.BlogViewComponents
{
    public class _BlogDetailTagCloudByBlogComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(string blogTagCloudData)
        {
            List<ResultBlogTagCloudForBlogDto> resultBlogTagCloudForBlogDto = new List<ResultBlogTagCloudForBlogDto>();

            if (blogTagCloudData != null)
            {
                resultBlogTagCloudForBlogDto = JsonConvert.DeserializeObject<List<ResultBlogTagCloudForBlogDto>>(blogTagCloudData);
            }

            return View(resultBlogTagCloudForBlogDto);
        }
    }
}
