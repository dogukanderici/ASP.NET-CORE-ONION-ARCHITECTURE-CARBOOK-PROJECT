using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarFeatureCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarFeatureHandlers
{
    public class UpdateCarFeatureCommandHandler
    {
        private readonly IRepository<CarFeature> _repository;
        private readonly IMapper _mapper;

        public UpdateCarFeatureCommandHandler(IRepository<CarFeature> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCarFeatureCommand updateCarFeatureCommand)
        {
            CarFeature valueFromDto = _mapper.Map<CarFeature>(updateCarFeatureCommand);

            await _repository.UpdateAsync(valueFromDto);
        }

    }
}
