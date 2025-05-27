using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.CarReviewQueries
{
    public class GetCarReviewByCarIdQuery
    {
        public int CarID { get; set; }

        public GetCarReviewByCarIdQuery(int carID)
        {
            CarID = carID;
        }
    }
}
