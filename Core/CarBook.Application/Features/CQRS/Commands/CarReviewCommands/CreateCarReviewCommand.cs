using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Commands.CarReviewCommands
{
    public class CreateCarReviewCommand
    {
        public Guid CarReviewID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int CommentStar { get; set; }
        public int CarID { get; set; }
        public DateTimeOffset ReviewDate { get; set; } = DateTimeOffset.Now;
        public bool IsAvailable { get; set; } = false;
    }
}
