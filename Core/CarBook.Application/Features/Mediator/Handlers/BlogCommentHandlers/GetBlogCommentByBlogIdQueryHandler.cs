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
    public class GetBlogCommentByBlogIdQueryHandler : IRequestHandler<GetBlogCommentByBlogIdQuery, List<GetBlogCommentByBlogIdQueryResult>>
    {
        private readonly IRepository<BlogComment> _repository;
        private readonly IMapper _mapper;

        public GetBlogCommentByBlogIdQueryHandler(IRepository<BlogComment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBlogCommentByBlogIdQueryResult>> Handle(GetBlogCommentByBlogIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BlogComment> dbQueryOptions = new DbQueryOptions<BlogComment>();

            Expression<Func<BlogComment, object>> shorting = bc => bc.CreatedDate;
            Expression<Func<BlogComment, bool>> filter = bc => bc.BlogID == request.Id;

            dbQueryOptions.shortingType = "descending";
            dbQueryOptions.shorting = shorting;
            dbQueryOptions.filter = filter;

            List<BlogComment> values = await _repository.GetAllAsync(dbQueryOptions);
            List<GetBlogCommentByBlogIdQueryResult> valueToDto = _mapper.Map<List<GetBlogCommentByBlogIdQueryResult>>(values);

            return valueToDto;
        }
    }
}
