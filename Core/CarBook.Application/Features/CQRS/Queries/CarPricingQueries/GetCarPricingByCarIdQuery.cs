﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.CarPricingQueries
{
    public class GetCarPricingByCarIdQuery
    {
        public int Id { get; set; }

        public GetCarPricingByCarIdQuery(int id)
        {
            Id = id;
        }
    }
}
