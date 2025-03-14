using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.TagCloudQueries;
using CarBook.Application.Features.Mediator.Results.TagCloudResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.TagCloudHandlers
{
    public class GetTagCloudByIdQueryHandler : IRequestHandler<GetTagCloudByIdQuery, GetTagCloudByIdQueryResult>
    {
        private readonly IRepository<TagCloud> _repository;
        private readonly IMapper _mapper;

        public GetTagCloudByIdQueryHandler(IRepository<TagCloud> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTagCloudByIdQueryResult> Handle(GetTagCloudByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<TagCloud> dbQueryOptions = new DbQueryOptions<TagCloud>();

            Expression<Func<TagCloud, bool>> filter = tc => (tc.TagStatus == true && tc.TagCloudID == request.Id);
            Expression<Func<TagCloud, object>> shorting = tc => tc.TagName;

            dbQueryOptions.filter = filter;
            dbQueryOptions.shorting = shorting;

            TagCloud values = await _repository.GetByIdAsync(dbQueryOptions);
            GetTagCloudByIdQueryResult valueToDto = _mapper.Map<GetTagCloudByIdQueryResult>(values);

            return valueToDto;
        }
    }
}
