using CarBook.Application.Features.Mediator.Results.BlogResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.BlogQueries
{
    public class GetBlogWithCountQuery : IRequest<GetBlogWithCountDataQueryResult>
    {
        public bool PublishState { get; set; }

        public GetBlogWithCountQuery(bool publishState)
        {
            PublishState = publishState;
        }
    }
}
