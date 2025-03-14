using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.FooterAddressQueries;
using CarBook.Application.Features.Mediator.Results.FooterAddressResults;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.FooterAddressHandlers
{
    public class GetFooterAddressQueryHandler : IRequestHandler<GetFooterAddressQuery, List<GetFooterAddressQueryResult>>
    {
        private readonly IRepository<FooterAddress> _repository;
        private readonly IMapper _mapper;

        public GetFooterAddressQueryHandler(IRepository<FooterAddress> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetFooterAddressQueryResult>> Handle(GetFooterAddressQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<FooterAddress> dbQueryOptions = new DbQueryOptions<FooterAddress>();

            List<FooterAddress> values = await _repository.GetAllAsync(dbQueryOptions);

            List<GetFooterAddressQueryResult> valueToDto = _mapper.Map<List<GetFooterAddressQueryResult>>(values);

            return valueToDto;
        }
    }
}
