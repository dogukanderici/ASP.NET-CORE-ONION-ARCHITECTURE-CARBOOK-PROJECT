using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.CarQueries
{
    public class GetCarQuery
    {
        public int SkipNumber { get; set; }
        public int TakeNumber { get; set; }

        public GetCarQuery(int? skipNumber, int? takeNumber)
        {
            SkipNumber = skipNumber ?? 0;
            TakeNumber = takeNumber ?? 0;
        }

        public GetCarQuery()
        {

        }
    }
}
