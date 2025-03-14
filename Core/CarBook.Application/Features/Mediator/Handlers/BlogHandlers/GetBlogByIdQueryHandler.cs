using AutoMapper;
using CarBook.Application.Features.CQRS.Queries.BlogCategoryQueries;
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
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, GetBlogByIdQueryResult>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;

        public GetBlogByIdQueryHandler(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBlogByIdQueryResult> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {

            DbQueryOptions<Blog> dbQueryOptions = new DbQueryOptions<Blog>();

            Expression<Func<Blog, bool>> filter = x => x.BlogID == request.Id;

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
                        b=>b.BlogTagClouds,
                        new List<Expression<Func<object, object>>>
                        {
                            btc=>((BlogTagCloud)btc).TagCloud
                        }
                    },
                    {
                        b=>b.BlogComments,
                        new List<Expression<Func<object, object>>>{}
                    }
                };

            dbQueryOptions.filter = filter;
            dbQueryOptions.thenIncludes = thenIncludes;

            Blog value = await _repository.GetByIdAsync(dbQueryOptions);

            GetBlogByIdQueryResult valueToDto = _mapper.Map<GetBlogByIdQueryResult>(value);

            return valueToDto;
        }
    }
}
