using CarBook.Application.Features.CQRS.Results.CategoryResults;
using CarBook.Application.Features.Mediator.Results.AuthorResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Commands.BlogCommands
{
    public class UpdateBlogCommand : IRequest
    {
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public Guid AuthorID { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int BlogCategoryID { get; set; }
        public bool PublishState { get; set; }
    }
}
