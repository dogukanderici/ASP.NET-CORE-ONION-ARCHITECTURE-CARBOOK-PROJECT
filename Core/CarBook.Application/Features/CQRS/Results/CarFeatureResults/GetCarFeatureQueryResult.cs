using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Features.Mediator.Results.FeatureResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Results.CarFeatureResults
{
    public class GetCarFeatureQueryResult
    {
        public int CarFeatureID { get; set; }
        public int CarID { get; set; }
        public GetCarForCarFeatureQueryResult Car { get; set; }
        public int FeatureID { get; set; }
        public GetFeatureQueryResult Feature { get; set; }
        public bool Available { get; set; }
    }
}
