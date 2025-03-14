using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.BlogCommentQueries;
using CarBook.Application.Features.Mediator.Results.BlogCommentResult;
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

namespace CarBook.Application.Features.Mediator.Handlers.BlogCommentHandlers
{
    public class GetBlogCommentQueryHandler : IRequestHandler<GetBlogCommentQuery, List<GetBlogCommentQueryResult>>
    {
        private readonly IRepository<BlogComment> _repository;
        private readonly IMapper _mapper;

        public GetBlogCommentQueryHandler(IRepository<BlogComment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBlogCommentQueryResult>> Handle(GetBlogCommentQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BlogComment> dbQueryOptions = new DbQueryOptions<BlogComment>();

            Expression<Func<BlogComment, object>> shorting = bc => bc.CreatedDate;
            List<Expression<Func<BlogComment, object>>> includes = new List<Expression<Func<BlogComment, object>>>
            {
                bc=>bc.Blog
            };

            dbQueryOptions.shortingType = "descending";
            dbQueryOptions.shorting = shorting;
            dbQueryOptions.includes = includes;

            List<BlogComment> values = await _repository.GetAllAsync(dbQueryOptions);
            List<GetBlogCommentQueryResult> valueToDto = _mapper.Map<List<GetBlogCommentQueryResult>>(values);

            return valueToDto;
        }
    }
}
