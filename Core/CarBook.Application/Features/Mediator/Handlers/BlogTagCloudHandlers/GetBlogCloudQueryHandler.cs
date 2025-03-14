using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.BlogTagCloudQueries;
using CarBook.Application.Features.Mediator.Results.BlogTagCloudResult;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.BlogTagCloudHandlers
{
    public class GetBlogCloudQueryHandler : IRequestHandler<GetBlogTagCloudQuery, List<GetBlogTagCloudQueryResult>>
    {
        private readonly IRepository<BlogTagCloud> _repository;
        private readonly IMapper _mapper;

        public GetBlogCloudQueryHandler(IRepository<BlogTagCloud> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBlogTagCloudQueryResult>> Handle(GetBlogTagCloudQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BlogTagCloud> dbQueryOptions = new DbQueryOptions<BlogTagCloud>();

            Expression<Func<BlogTagCloud, object>> shorting = btc => btc.Blog.CreatedDate;
            Dictionary<Expression<Func<BlogTagCloud, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<BlogTagCloud, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        btc=>btc.Blog,
                        new List<Expression<Func<object, object>>>{
                            blogCategory => ((Blog)blogCategory).BlogCategory
                        }
                    },
                    {
                        btc=>btc.TagCloud,
                        new List<Expression<Func<object, object>>>{}                    }
                };

            dbQueryOptions.shorting = shorting;
            dbQueryOptions.thenIncludes = thenIncludes;

            List<BlogTagCloud> values = await _repository.GetAllAsync(dbQueryOptions);
            List<GetBlogTagCloudQueryResult> valueToDto = _mapper.Map<List<GetBlogTagCloudQueryResult>>(values);

            return valueToDto;
        }
    }
}
