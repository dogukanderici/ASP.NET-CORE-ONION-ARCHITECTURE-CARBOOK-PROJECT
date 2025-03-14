using AutoMapper;
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
    public class GetBlogWithCommentQueryHandler : IRequestHandler<GetblogWithCommentQuery, GetBlogWithCommentQueryResult>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;

        public GetBlogWithCommentQueryHandler(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBlogWithCommentQueryResult> Handle(GetblogWithCommentQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Blog> dbQueryOptions = new DbQueryOptions<Blog>();

            Expression<Func<Blog, bool>> filter = b => b.BlogID == request.Id;
            Dictionary<Expression<Func<Blog, object>>, List<Expression<Func<object, object>>>> thenIncludes =
                new Dictionary<Expression<Func<Blog, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        b=>b.Author,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        b=>b.BlogCategory,
                        new List<Expression<Func<object, object>>>{}
                    },
                    {
                        b=>b.BlogComments,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            Blog value = await _repository.GetByIdAsync(dbQueryOptions);

            GetBlogWithCommentQueryResult valueToDto = _mapper.Map<GetBlogWithCommentQueryResult>(value);

            return valueToDto;
        }
    }
}
