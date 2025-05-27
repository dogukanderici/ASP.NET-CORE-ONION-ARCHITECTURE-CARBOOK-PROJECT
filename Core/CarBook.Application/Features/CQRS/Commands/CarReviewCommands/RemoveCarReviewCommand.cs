using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Commands.CarReviewCommands
{
    public class RemoveCarReviewCommand
    {
        public Guid CarReviewID { get; set; }

        public RemoveCarReviewCommand(Guid carReviewID)
        {
            CarReviewID = carReviewID;
        }
    }
}
