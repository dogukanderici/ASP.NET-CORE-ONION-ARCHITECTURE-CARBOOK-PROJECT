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
    public class CreateCarReviewCommandHandler
    {
        private readonly IRepository<CarReview> _repository;
        private readonly IMapper _mapper;

        public CreateCarReviewCommandHandler(IRepository<CarReview> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateCarReviewCommand createCarReviewCommand)
        {
            createCarReviewCommand.CarReviewID = Guid.NewGuid();
            createCarReviewCommand.ReviewDate = DateTimeOffset.Now;
            createCarReviewCommand.IsAvailable = false;

            CarReview valueFromDto = _mapper.Map<CarReview>(createCarReviewCommand);

            await _repository.CreateAsync(valueFromDto);
        }
    }
}
