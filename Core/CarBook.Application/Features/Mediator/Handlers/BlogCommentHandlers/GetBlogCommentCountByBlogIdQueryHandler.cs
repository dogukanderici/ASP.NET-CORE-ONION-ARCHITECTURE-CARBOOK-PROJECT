using CarBook.Application.Features.Mediator.Queries.BlogCommentQueries;
using CarBook.Application.Features.Mediator.Results.BlogCommentResult;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.BlogCommentHandlers
{
    public class GetBlogCommentCountByBlogIdQueryHandler : IRequestHandler<GetBlogCommentCountByBlogIdQuery, GetBlogCommentCountByBlogIdQueryResult>
    {
        private readonly IRepository<BlogComment> _repository;

        public GetBlogCommentCountByBlogIdQueryHandler(IRepository<BlogComment> repository)
        {
            _repository = repository;
        }

        public async Task<GetBlogCommentCountByBlogIdQueryResult> Handle(GetBlogCommentCountByBlogIdQuery request, CancellationToken cancellationToken)
        {
            var blogCommentCount = _repository.GetQueryableEntity(new DbQueryOptions<BlogComment>())
                .Where(b => request.BlogIDList.Contains(b.BlogID))
                .GroupBy(b => b.BlogID)
                .Select(g => new { BlogID = g.Key, Count = g.Count() })
                .ToDictionary(x => x.BlogID, x => x.Count);

            GetBlogCommentCountByBlogIdQueryResult getBlogCommentCountByBlogIdQueryResult = new GetBlogCommentCountByBlogIdQueryResult
            {
                BlogCommentCount = blogCommentCount
            };

            return getBlogCommentCountByBlogIdQueryResult;
        }
    }
}
