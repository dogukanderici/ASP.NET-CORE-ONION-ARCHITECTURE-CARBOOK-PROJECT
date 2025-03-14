using CarBook.Application.Features.Mediator.Results.BlogResults;
using CarBook.Application.Features.Mediator.Results.TagCloudResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogTagCloudResult
{
    public class GetBlogTagCloudForBlogQueryResult
    {
        public Guid BlogTagCloudID { get; set; }
        public Guid TagCloudID { get; set; }
        public GetTagCloudQueryResult TagCloud { get; set; }
    }
}
