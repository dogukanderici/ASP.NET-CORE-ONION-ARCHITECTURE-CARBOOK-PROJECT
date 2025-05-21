using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.BlogCommentQueries;
using CarBook.Application.Features.Mediator.Queries.BlogQueries;
using CarBook.Application.Features.Mediator.Results.BlogResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class GetBlogWithPublishStateQueryHandler : IRequestHandler<GetBlogWithPublishStateQuery, List<GetBlogWithPublishStateQueryResult>>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetBlogWithPublishStateQueryHandler(IRepository<Blog> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<GetBlogWithPublishStateQueryResult>> Handle(GetBlogWithPublishStateQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Blog, object>>> includes = new List<Expression<Func<Blog, object>>>
            {
                b => b.Author
            };

            Expression<Func<Blog, bool>> filter = b => b.PublishState == request.PublishState;

            DbQueryOptions<Blog> dbQueryOptions = new DbQueryOptions<Blog>();

            dbQueryOptions.filter = filter;
            dbQueryOptions.includes = includes;

            List<Blog> values = _repository.GetQueryableEntity(dbQueryOptions)
                .Skip((request.PageDataSize * (request.PageNumber - 1)))
                .Take(request.PageDataSize).ToList();

            var blogIdList = values.Select(b => b.BlogID).ToList();

            var blogCommentCount = await _mediator.Send(new GetBlogCommentCountByBlogIdQuery(blogIdList));

            List<GetBlogWithPublishStateQueryResult> valueToDto = _mapper.Map<List<GetBlogWithPublishStateQueryResult>>(values);

            foreach (var item in valueToDto)
            {
                item.BlogCommentCount = blogCommentCount.BlogCommentCount.ContainsKey(item.BlogID) ? blogCommentCount.BlogCommentCount[item.BlogID] : 0;
            }

            return valueToDto;

        }
    }
}
