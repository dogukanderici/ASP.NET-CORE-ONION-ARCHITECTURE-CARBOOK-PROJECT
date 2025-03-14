using CarBook.Application.Features.Mediator.Results.BlogResults;
using CarBook.Application.Features.Mediator.Results.TagCloudResults;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogTagCloudResult
{
    public class GetBlogTagCloudQueryResult
    {
        public Guid BlogTagCloudID { get; set; }
        public Guid BlogID { get; set; }
        public GetBlogQueryResult Blog { get; set; }
        public Guid TagCloudID { get; set; }
        public GetTagCloudQueryResult TagCloud { get; set; }
    }
}
