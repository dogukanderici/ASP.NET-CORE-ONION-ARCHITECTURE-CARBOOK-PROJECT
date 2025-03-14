using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.AuthorCommands;
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
    public class RemoveAuthorCommandHandler : IRequestHandler<RemoveAuthorCommand>
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;

        public RemoveAuthorCommandHandler(IRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Author> dbQueryOptions = new DbQueryOptions<Author>();

            Expression<Func<Author, bool>> filter = x => x.AuthorID == request.Id;

            dbQueryOptions.filter = filter;

            Author value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
