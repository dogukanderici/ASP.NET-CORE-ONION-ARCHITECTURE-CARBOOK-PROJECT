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
        public bool? Status { get; set; }

        public GetCarReviewByCarIdQuery(int carID, bool? status)
        {
            CarID = carID;
            Status = status;
        }
    }
}
