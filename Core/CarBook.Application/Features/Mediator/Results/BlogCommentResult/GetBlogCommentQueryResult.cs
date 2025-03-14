using CarBook.Application.Features.Mediator.Results.BlogResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogCommentResult
{
    public class GetBlogCommentQueryResult
    {
        public Guid BlogCommentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid BlogID { get; set; }
        public GetBlogQueryResult Blog { get; set; }
    }
}
