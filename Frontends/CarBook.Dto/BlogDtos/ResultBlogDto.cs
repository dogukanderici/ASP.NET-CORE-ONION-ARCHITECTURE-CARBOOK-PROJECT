using CarBook.Dto.AuthorDtos;
using CarBook.Dto.BlogCategoryDtos;
using CarBook.Dto.BlogCommentDtos;
using CarBook.Dto.BlogTagCloudDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.BlogDtos
{
    public class ResultBlogDto
    {
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public Guid AuthorID { get; set; }
        public ResultAuthorDto Author { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int BlogCategoryID { get; set; }
        public int BlogCommentCount { get; set; }
        public bool PublishState { get; set; }
        public ResultBlogCategoryDto BlogCategory { get; set; }
        public List<ResultBlogTagCloudForBlogDto> BlogTagClouds { get; set; }
        public List<ResultBlogCommentDto> BlogComments { get; set; }
    }
}
