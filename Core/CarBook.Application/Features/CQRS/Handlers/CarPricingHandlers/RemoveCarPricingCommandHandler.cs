using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarPricingCommands;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarPricingHandlers
{
    public class RemoveCarPricingCommandHandler
    {
        private readonly IRepository<CarPricing> _repository;

        public RemoveCarPricingCommandHandler(IRepository<CarPricing> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveCarPricingCommand removeCarPricingCommand)
        {
            DbQueryOptions<CarPricing> dbQueryOptions = new DbQueryOptions<CarPricing>();

            Expression<Func<CarPricing, bool>> filter = x => x.CarPricingID == removeCarPricingCommand.Id;

            dbQueryOptions.filter = filter;

            CarPricing value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
