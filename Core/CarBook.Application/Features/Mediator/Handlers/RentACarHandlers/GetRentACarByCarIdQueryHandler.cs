using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.RentACarQueries;
using CarBook.Application.Features.Mediator.Results.RentACarResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.RentACarHandlers
{
    public class GetRentACarByCarIdQueryHandler : IRequestHandler<GetRentACarByCarIdQuery, GetRentACarByCarIdQueryResult>
    {
        private readonly IRepository<RentACar> _repository;
        private readonly IMapper _mapper;

        public GetRentACarByCarIdQueryHandler(IRepository<RentACar> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetRentACarByCarIdQueryResult> Handle(GetRentACarByCarIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<RentACar> dbQueryOptions = new DbQueryOptions<RentACar>();

            Expression<Func<RentACar, bool>> filter = x => x.CarID == request.Id;

            dbQueryOptions.filter = filter;

            RentACar value = await _repository.GetByIdAsync(dbQueryOptions);

            GetRentACarByCarIdQueryResult valueToDto = _mapper.Map<GetRentACarByCarIdQueryResult>(value);

            return valueToDto;
        }
    }
}
