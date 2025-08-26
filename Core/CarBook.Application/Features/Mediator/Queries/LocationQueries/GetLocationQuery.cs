using CarBook.Application.Features.Mediator.Results.LocationResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.LocationQueries
{
    public class GetLocationQuery : IRequest<GetLocationDataQueryResult>
    {
        public int SkipNumber { get; set; }
        public int TakeNumber { get; set; }

        public GetLocationQuery(int? skipNumber, int? takeNumber)
        {
            SkipNumber = skipNumber ?? 0;
            TakeNumber = takeNumber ?? 0;
        }

        public GetLocationQuery()
        {

        }
    }
}
