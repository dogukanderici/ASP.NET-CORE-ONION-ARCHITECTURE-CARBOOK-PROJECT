using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Queries.CarReviewQueries
{
    public class GetCarReviewByIdQuery
    {
        public Guid CarReviewID { get; set; }

        public GetCarReviewByIdQuery(Guid carReviewID)
        {
            CarReviewID = carReviewID;
        }
    }
}
