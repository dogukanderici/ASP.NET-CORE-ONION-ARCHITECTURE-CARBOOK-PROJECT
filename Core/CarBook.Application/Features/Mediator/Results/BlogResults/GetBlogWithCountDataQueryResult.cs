using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Results.BlogResults
{
    public class GetBlogWithCountDataQueryResult
    {
        public GetBlogWithCountDataQueryResult()
        {
            TotalBlogCount = 0;
        }
        public int TotalBlogCount { get; set; }
    }
}
