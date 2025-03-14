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
    public class GetBlogTagCloudByIdQueryHandler : IRequestHandler<GetBlogTagCloudByIdQuery, GetBlogTagCloudByIdQueryResult>
    {
        private readonly IRepository<BlogTagCloud> _repository;
        private readonly IMapper _mapper;

        public GetBlogTagCloudByIdQueryHandler(IRepository<BlogTagCloud> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBlogTagCloudByIdQueryResult> Handle(GetBlogTagCloudByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BlogTagCloud> dbQueryOptions = new DbQueryOptions<BlogTagCloud>();

            Expression<Func<BlogTagCloud, bool>> filter = btc => btc.BlogID == request.Id;
            Expression<Func<BlogTagCloud, object>> shorting = btc => btc.Blog.CreatedDate;

            Dictionary<Expression<Func<BlogTagCloud, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<BlogTagCloud, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        btc=>btc.Blog,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        btc=>btc.TagCloud,
                        new List<Expression<Func<object, object>>>{}                    }
                };

            dbQueryOptions.shorting = shorting;
            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            BlogTagCloud values = await _repository.GetByIdAsync(dbQueryOptions);
            GetBlogTagCloudByIdQueryResult valueToDto = _mapper.Map<GetBlogTagCloudByIdQueryResult>(values);

            return valueToDto;
        }
    }
}
