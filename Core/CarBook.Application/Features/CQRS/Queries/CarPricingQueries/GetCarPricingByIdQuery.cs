using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.CarPricingQueries
{
    public class GetCarPricingByIdQuery
    {
        public int Id { get; set; }

        public GetCarPricingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
