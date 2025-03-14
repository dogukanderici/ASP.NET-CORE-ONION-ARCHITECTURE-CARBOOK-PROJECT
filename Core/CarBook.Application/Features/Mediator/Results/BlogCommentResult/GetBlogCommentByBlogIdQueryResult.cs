using CarBook.Application.Features.Mediator.Results.BlogResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogCommentResult
{
    public class GetBlogCommentByBlogIdQueryResult
    {
        public Guid BlogCommentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid BlogID { get; set; }
    }
}
