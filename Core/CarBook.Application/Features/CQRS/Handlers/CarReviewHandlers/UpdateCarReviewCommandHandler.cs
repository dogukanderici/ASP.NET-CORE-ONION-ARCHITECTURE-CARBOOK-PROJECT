using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarReviewCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarReviewHandlers
{
    public class UpdateCarReviewCommandHandler
    {
        private readonly IRepository<CarReview> _repository;
        private readonly IMapper _mapper;

        public UpdateCarReviewCommandHandler(IRepository<CarReview> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCarReviewCommand updateCarReviewCommand)
        {

            CarReview valueFromDto = _mapper.Map<CarReview>(updateCarReviewCommand);

            await _repository.UpdateAsync(valueFromDto);
        }
    }
}
