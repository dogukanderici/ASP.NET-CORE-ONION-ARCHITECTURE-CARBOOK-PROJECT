using CarBook.Dto.BlogCommentDtos;

namespace CarBook.WebUI.Services.BlogCommentServices
{
    public interface IBlogCommentService
    {
        Task<bool> CreateNewBlogCommentAsync(CreateBlogCommentDto createBlogCommentDto);
    }
}
