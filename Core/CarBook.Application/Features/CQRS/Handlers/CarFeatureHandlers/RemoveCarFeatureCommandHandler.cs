using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarFeatureCommands;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarFeatureHandlers
{
    public class RemoveCarFeatureCommandHandler
    {
        private readonly IRepository<CarFeature> _repository;

        public RemoveCarFeatureCommandHandler(IRepository<CarFeature> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveCarFeatureCommand removeCarFeatureCommand)
        {
            DbQueryOptions<CarFeature> dbQueryOptions = new DbQueryOptions<CarFeature>();

            Expression<Func<CarFeature, bool>> filter = cf => cf.CarFeatureID == removeCarFeatureCommand.Id;
            dbQueryOptions.filter = filter;

            CarFeature value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }

    }
}
