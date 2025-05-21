using CarBook.Application.Features.Mediator.Results.BlogResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.BlogQueries
{
    public class GetBlogWithPublishStateQuery : IRequest<List<GetBlogWithPublishStateQueryResult>>
    {
        public bool PublishState { get; set; }
        public int PageDataSize { get; set; }
        public int PageNumber { get; set; }

        public GetBlogWithPublishStateQuery(bool publishState, int pageDataSize, int pageNumber)
        {
            PublishState = publishState;
            PageDataSize = pageDataSize;
            PageNumber = pageNumber;
        }
    }
}
