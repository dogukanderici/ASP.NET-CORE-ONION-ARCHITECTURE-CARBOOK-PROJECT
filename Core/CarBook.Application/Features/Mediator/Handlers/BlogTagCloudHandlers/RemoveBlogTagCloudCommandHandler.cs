using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands;
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

namespace CarBook.Application.Features.Mediator.Handlers.BlogTagCloudHandlers
{
    public class RemoveBlogTagCloudCommandHandler : IRequestHandler<RemoveBlogTagCloudCommand>
    {
        private readonly IRepository<BlogTagCloud> _repository;

        public RemoveBlogTagCloudCommandHandler(IRepository<BlogTagCloud> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBlogTagCloudCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<BlogTagCloud> dbQueryOptions = new DbQueryOptions<BlogTagCloud>();

            Expression<Func<BlogTagCloud, bool>> filter = btc => btc.BlogID == request.Id;

            dbQueryOptions.filter = filter;

            BlogTagCloud value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
