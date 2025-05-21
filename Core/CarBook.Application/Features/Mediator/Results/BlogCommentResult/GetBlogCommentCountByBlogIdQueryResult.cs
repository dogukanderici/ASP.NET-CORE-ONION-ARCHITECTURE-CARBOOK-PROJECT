using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogCommentResult
{
    public class GetBlogCommentCountByBlogIdQueryResult
    {
        public GetBlogCommentCountByBlogIdQueryResult()
        {
            BlogCommentCount = new Dictionary<Guid, int>();
        }

        public Dictionary<Guid, int> BlogCommentCount { get; set; }
    }
}
