using CarBook.Application.Features.Mediator.Results.BlogCommentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.BlogCommentQueries
{
    public class GetBlogCommentCountByBlogIdQuery : IRequest<GetBlogCommentCountByBlogIdQueryResult>
    {
        public List<Guid> BlogIDList { get; set; }

        public GetBlogCommentCountByBlogIdQuery(List<Guid> blogIDList)
        {
            BlogIDList = blogIDList;
        }
    }
}
