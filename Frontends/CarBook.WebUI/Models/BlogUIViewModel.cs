using CarBook.Dto.BlogCommentDtos;
using CarBook.Dto.BlogDtos;

namespace CarBook.WebUI.Models
{
    public class BlogUIViewModel
    {
        public BlogUIViewModel()
        {
            BlogDatas = new List<ResultBlogDto>();
            BlogByIdData = new ResultBlogDto();
        }

        public List<ResultBlogDto> BlogDatas { get; set; }
        public ResultBlogDto BlogByIdData { get; set; }
        public CreateBlogCommentDto CreateBlogCommentData { get; set; }

        public int PageDataSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalData { get; set; }
    }
}