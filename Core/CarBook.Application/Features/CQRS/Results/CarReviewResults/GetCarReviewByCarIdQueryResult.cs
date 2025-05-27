using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Results.CarReviewResults
{
    public class GetCarReviewByCarIdQueryResult
    {
        public Guid CarReviewID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int CommentStar { get; set; }
        public int CarID { get; set; }
        public DateTimeOffset ReviewDate { get; set; }
        public bool IsAvailable { get; set; }

        public GetCarByIdForCarReviewQueryResult GetCarByIdForCarReviewQueryResult { get; set; }
    }
}
