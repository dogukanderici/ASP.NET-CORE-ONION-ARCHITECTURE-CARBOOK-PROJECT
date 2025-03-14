using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.PricingTypeQueries;
using CarBook.Application.Features.Mediator.Results.PricingTypeResults;
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
    public class GetPricingTypeByIdQueryHandler : IRequestHandler<GetPricingTypeByIdQuery, GetPricingTypeByIdQueryResult>
    {
        private readonly IRepository<PricingType> _repository;
        private readonly IMapper _mapper;

        public GetPricingTypeByIdQueryHandler(IRepository<PricingType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetPricingTypeByIdQueryResult> Handle(GetPricingTypeByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<PricingType> dbQueryOptions = new DbQueryOptions<PricingType>();

            Expression<Func<PricingType, bool>> filter = x => x.PricingTypeID == request.Id;

            dbQueryOptions.filter = filter;

            PricingType value = await _repository.GetByIdAsync(dbQueryOptions);

            GetPricingTypeByIdQueryResult valueToDto = _mapper.Map<GetPricingTypeByIdQueryResult>(value);

            return valueToDto;
        }
    }
}
