using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands
{
    public class UpdateBlogTagCloudCommand : IRequest
    {
        public Guid BlogTagCloudID { get; set; }
        public Guid BlogID { get; set; }
        public Guid TagCloudID { get; set; }
    }
}
