
using CarBook.Dto.BlogCommentDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class AdminUIBlogCommentViewModel
    {
        public List<ResultBlogCommentDto> ResultDatas { get; set; }
        public CreateBlogCommentDto CreateData { get; set; }
        public UpdateBlogCommentDto UpdateData { get; set; }
    }
}
