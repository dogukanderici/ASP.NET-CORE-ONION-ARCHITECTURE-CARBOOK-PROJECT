using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.PricingTypeCommands;
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

namespace CarBook.Application.Features.Mediator.Handlers.PricingTypeHandlers
{
    public class RemovePricingTypeCommandHandler : IRequestHandler<RemovePricingTypeCommand>
    {
        private readonly IRepository<PricingType> _repository;

        public RemovePricingTypeCommandHandler(IRepository<PricingType> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemovePricingTypeCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<PricingType> dbQueryOptions = new DbQueryOptions<PricingType>();

            Expression<Func<PricingType, bool>> filter = x => x.PricingTypeID == request.Id;

            dbQueryOptions.filter = filter;

            PricingType value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
