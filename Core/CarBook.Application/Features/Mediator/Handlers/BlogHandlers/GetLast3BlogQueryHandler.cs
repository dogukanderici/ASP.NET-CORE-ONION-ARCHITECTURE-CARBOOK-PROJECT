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
    public class GetLast3BlogQueryHandler : IRequestHandler<GetLast3BlogQuery, List<GetLast3BlogQueryResult>>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetLast3BlogQueryHandler(IRepository<Blog> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<GetLast3BlogQueryResult>> Handle(GetLast3BlogQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Blog, object>>> includeList = new List<Expression<Func<Blog, object>>>
            {
                b=>b.Author,
                b=>b.BlogCategory
            };

            Expression<Func<Blog, object>> shorting = b => b.CreatedDate;

            DbQueryOptions<Blog> dbQueryOptions = new DbQueryOptions<Blog>();
            dbQueryOptions.includes = includeList;
            dbQueryOptions.shortingType = "descending";
            dbQueryOptions.shorting = shorting;
            dbQueryOptions.DataTakeNumber = 3;

            List<Blog> values = await _repository.GetAllAsync(dbQueryOptions);

            var blogIds = values.Select(b => b.BlogID).ToList();

            var blogCommentValue = await _mediator.Send(new GetBlogCommentCountByBlogIdQuery(blogIds));

            List<GetLast3BlogQueryResult> valueToDto = _mapper.Map<List<GetLast3BlogQueryResult>>(values);

            foreach (var item in valueToDto)
            {
                item.BlogCommentCount = blogCommentValue.BlogCommentCount.ContainsKey(item.BlogID) ? blogCommentValue.BlogCommentCount[item.BlogID] : 0;
            }

            return valueToDto;
        }
    }
}
