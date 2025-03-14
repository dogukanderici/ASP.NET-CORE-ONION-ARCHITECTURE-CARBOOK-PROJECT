using CarBook.Application.Features.Mediator.Results.BlogCommentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.BlogCommentQueries
{
    public class GetBlogCommentByBlogIdQuery : IRequest<List<GetBlogCommentByBlogIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetBlogCommentByBlogIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
