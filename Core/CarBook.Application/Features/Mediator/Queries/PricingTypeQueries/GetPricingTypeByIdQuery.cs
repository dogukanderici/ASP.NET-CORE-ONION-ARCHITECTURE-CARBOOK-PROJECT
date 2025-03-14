using CarBook.Application.Features.Mediator.Results.PricingTypeResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Queries.PricingTypeQueries
{
    public class GetPricingTypeByIdQuery : IRequest<GetPricingTypeByIdQueryResult>
    {
        public int Id { get; set; }

        public GetPricingTypeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
