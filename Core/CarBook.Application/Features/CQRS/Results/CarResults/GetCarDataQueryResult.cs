using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Results.CarResults
{
    public class GetCarDataQueryResult
    {
        public List<GetCarQueryResult> CarDatas { get; set; }
        public int TotalDataCount { get; set; }
    }
}
