﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.CarFeatureQueries
{
    public class GetCarFeatureByIdQuery
    {
        public int Id { get; set; }

        public GetCarFeatureByIdQuery(int id)
        {
            Id = id;
        }
    }
}
