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
    public class GetBlogWithCountDataQueryHandler : IRequestHandler<GetBlogWithCountQuery, GetBlogWithCountDataQueryResult>
    {
        private readonly IRepository<Blog> _repository;

        public GetBlogWithCountDataQueryHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<GetBlogWithCountDataQueryResult> Handle(GetBlogWithCountQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Blog> DbQueryOptions = new DbQueryOptions<Blog>();

            Expression<Func<Blog, bool>> filter = x => x.PublishState == request.PublishState;
            DbQueryOptions.filter = filter;

            // Toplam Blog Sayısını Döner
            var totalBlogCount = _repository.GetQueryableEntity(DbQueryOptions).Count();

            return new GetBlogWithCountDataQueryResult
            {
                TotalBlogCount = totalBlogCount
            };
        }
    }
}
