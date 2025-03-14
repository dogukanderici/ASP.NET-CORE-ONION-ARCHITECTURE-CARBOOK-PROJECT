using CarBook.Application.Features.Mediator.Results.BlogResults;
using CarBook.Application.Features.Mediator.Results.TagCloudResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands
{
    public class CreateBlogTagCloudCommand : IRequest
    {
        public Guid BlogID { get; set; }
        public Guid TagCloudID { get; set; }
    }
}
