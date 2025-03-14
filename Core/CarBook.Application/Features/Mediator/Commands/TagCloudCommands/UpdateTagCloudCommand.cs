using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.TagCloudCommands
{
    public class UpdateTagCloudCommand : IRequest
    {
        public Guid TagCloudID { get; set; }
        public string TagName { get; set; }
        public bool TagStatus { get; set; }
    }
}
