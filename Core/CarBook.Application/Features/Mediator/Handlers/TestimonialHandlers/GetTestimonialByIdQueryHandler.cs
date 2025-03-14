using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.TestimonialQueries;
using CarBook.Application.Features.Mediator.Results.TestimonialResults;
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
    public class GetTestimonialByIdQueryHandler : IRequestHandler<GetTestimonialByIdQuery, GetTestimonialByIdQueryResult>
    {
        private readonly IRepository<Testimonial> _repository;
        private readonly IMapper _mapper;

        public GetTestimonialByIdQueryHandler(IRepository<Testimonial> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Testimonial> dbQueryOptions = new DbQueryOptions<Testimonial>();

            Expression<Func<Testimonial, bool>> filter = x => x.TestimonialID == request.Id;

            dbQueryOptions.filter = filter;

            Testimonial value = await _repository.GetByIdAsync(dbQueryOptions);

            GetTestimonialByIdQueryResult valueToDto = _mapper.Map<GetTestimonialByIdQueryResult>(value);

            return valueToDto;
        }
    }
}
