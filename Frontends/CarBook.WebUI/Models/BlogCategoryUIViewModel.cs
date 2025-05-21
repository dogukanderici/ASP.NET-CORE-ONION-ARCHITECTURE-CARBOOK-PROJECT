using CarBook.Dto.BlogCategoryDtos;

namespace CarBook.WebUI.Models
{
    public class BlogCategoryUIViewModel
    {
        public BlogCategoryUIViewModel()
        {
            BlogCategoryDatas = new List<ResultBlogCategoryDto>();
        }

        public List<ResultBlogCategoryDto> BlogCategoryDatas { get; set; }
    }
}
