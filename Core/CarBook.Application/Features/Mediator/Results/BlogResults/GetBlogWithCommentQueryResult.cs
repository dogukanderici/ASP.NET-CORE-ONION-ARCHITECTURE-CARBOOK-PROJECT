using CarBook.Application.Features.CQRS.Results.CategoryResults;
using CarBook.Application.Features.Mediator.Results.AuthorResults;
using CarBook.Application.Features.Mediator.Results.BlogCommentResult;
using CarBook.Application.Features.Mediator.Results.BlogTagCloudResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogResults
{
    public class GetBlogWithCommentQueryResult
    {
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public Guid AuthorID { get; set; }
        public GetAuthorQueryResult Author { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int BlogCategoryID { get; set; }
        public bool PublishState { get; set; } = true;
        public GetBlogCategoryQueryResult BlogCategory { get; set; }
        public List<GetBlogTagCloudForBlogQueryResult> BlogTagClouds { get; set; }
        public List<GetBlogCommentForBlogQueryResult> BlogComments { get; set; }
    }
}
