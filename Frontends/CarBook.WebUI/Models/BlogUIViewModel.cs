using CarBook.Dto.BlogDtos;

namespace CarBook.WebUI.Models
{
    public class BlogUIViewModel
    {
        public List<ResultBlogDto> BlogDatas { get; set; }
        public ResultBlogDto BlogByIdData { get; set; }
    }
}