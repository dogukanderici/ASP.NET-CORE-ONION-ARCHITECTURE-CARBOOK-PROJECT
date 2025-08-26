using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.LocationResults
{
    public class GetLocationDataQueryResult
    {
        public List<GetLocationQueryResult> Locations { get; set; }
        public int LocationCount { get; set; }
    }
}
