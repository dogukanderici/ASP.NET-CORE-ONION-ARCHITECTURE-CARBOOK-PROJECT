using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Dto.CarReviewDtos
{
    public class CreateCarReviewDto
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
    }
}
