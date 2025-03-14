using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.AuthorQueries;
using CarBook.Application.Features.Mediator.Results.AuthorResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.AuthorHandlers
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, GetAuthorByIdQueryResult>
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryHandler(IRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAuthorByIdQueryResult> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Author> dbQueryOptions = new DbQueryOptions<Author>();

            Expression<Func<Author, bool>> filter = x => x.AuthorID == request.Id;

            dbQueryOptions.filter = filter;

            Author value = await _repository.GetByIdAsync(dbQueryOptions);

            GetAuthorByIdQueryResult valueToDto = _mapper.Map<GetAuthorByIdQueryResult>(value);

            return valueToDto;
        }
    }
}
