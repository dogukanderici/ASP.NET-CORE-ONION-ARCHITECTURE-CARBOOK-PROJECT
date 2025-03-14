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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.FooterAddressHandlers
{
    public class GetFooterAddressByIdQueryHandler : IRequestHandler<GetFooterAddressByIdQuery, GetFooterAddressByIdQueryResult>
    {
        private readonly IRepository<FooterAddress> _repository;
        private readonly IMapper _mapper;

        public GetFooterAddressByIdQueryHandler(IRepository<FooterAddress> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetFooterAddressByIdQueryResult> Handle(GetFooterAddressByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<FooterAddress> dbQueryOptions = new DbQueryOptions<FooterAddress>();

            Expression<Func<FooterAddress, bool>> filter = x => x.FooterAddressID == request.Id;

            dbQueryOptions.filter = filter;

            FooterAddress value = await _repository.GetByIdAsync(dbQueryOptions);

            GetFooterAddressByIdQueryResult valueToDto = _mapper.Map<GetFooterAddressByIdQueryResult>(value);

            return valueToDto;
        }
    }
}
