using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.TestimonialCommands;
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

namespace CarBook.Application.Features.Mediator.Handlers.TestimonialHandlers
{
    public class RemoveTestimonialCommandHandler : IRequestHandler<RemoveTestimonialCommand>
    {
        private readonly IRepository<Testimonial> _repository;

        public RemoveTestimonialCommandHandler(IRepository<Testimonial> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveTestimonialCommand request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Testimonial> dbQueryOptions = new DbQueryOptions<Testimonial>();

            Expression<Func<Testimonial, bool>> filter = x => x.TestimonialID == request.Id;

            dbQueryOptions.filter = filter;

            Testimonial value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
