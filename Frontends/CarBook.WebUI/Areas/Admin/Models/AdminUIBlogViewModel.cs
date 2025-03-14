using CarBook.Dto.BlogDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIBlogViewModel
    {
        public List<ResultBlogDto> ResultDatas { get; set; }
        public CreateBlogDto CreateData { get; set; }
        public UpdateBlogDto UpdateData { get; set; }
    }
}
