using CarBook.Application.Features.Mediator.Results.BlogTagCloudResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.BlogTagCloudQueries
{
    public class GetBlogTagCloudQuery : IRequest<List<GetBlogTagCloudQueryResult>>
    {
    }
}
