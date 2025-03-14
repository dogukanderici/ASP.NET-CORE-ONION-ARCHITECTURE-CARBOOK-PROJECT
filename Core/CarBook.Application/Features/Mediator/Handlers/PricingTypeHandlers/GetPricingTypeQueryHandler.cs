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
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.PricingTypeHandlers
{
    public class GetPricingTypeQueryHandler : IRequestHandler<GetPricingTypeQuery, List<GetPricingTypeQueryResult>>
    {
        private readonly IRepository<PricingType> _repository;
        private readonly IMapper _mapper;

        public GetPricingTypeQueryHandler(IRepository<PricingType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetPricingTypeQueryResult>> Handle(GetPricingTypeQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<PricingType> dbQueryOptions = new DbQueryOptions<PricingType>();

            List<PricingType> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetPricingTypeQueryResult> valueToDto = _mapper.Map<List<GetPricingTypeQueryResult>>(values);

            return valueToDto;
        }
    }
}
