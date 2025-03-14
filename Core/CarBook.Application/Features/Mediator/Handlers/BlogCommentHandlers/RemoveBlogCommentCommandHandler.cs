using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogCommands;
using CarBook.Application.Features.Mediator.Commands.BlogCommentCommands;
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
    public class RemoveBlogCommentCommandHandler : IRequestHandler<RemoveBlogCommentCommand>
    {
        private readonly IRepository<BlogComment> _repository;
        public RemoveBlogCommentCommandHandler(IRepository<BlogComment> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBlogCommentCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BlogComment> dbQueryOptions = new DbQueryOptions<BlogComment>();

            Expression<Func<BlogComment, bool>> filter = bc => bc.BlogCommentID == request.Id;

            dbQueryOptions.filter = filter;

            BlogComment value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
