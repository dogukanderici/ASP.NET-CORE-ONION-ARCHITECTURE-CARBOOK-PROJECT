using CarBook.Dto.BlogCategoryDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIBlogCategoryViewModel
    {
        public List<ResultBlogCategoryDto> ResultDatas { get; set; }
        public CreateBlogCategoryDto CreateData { get; set; }
        public UpdateBlogCategoryDto UpdateData { get; set; }
    }
}
