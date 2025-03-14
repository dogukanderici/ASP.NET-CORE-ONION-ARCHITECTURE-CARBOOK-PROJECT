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
    public class GetTagCloudQueryHandler : IRequestHandler<GetTagCloudQuery, List<GetTagCloudQueryResult>>
    {
        private readonly IRepository<TagCloud> _repository;
        private readonly IMapper _mapper;

        public GetTagCloudQueryHandler(IRepository<TagCloud> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetTagCloudQueryResult>> Handle(GetTagCloudQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<TagCloud> dbQueryOptions = new DbQueryOptions<TagCloud>();

            Expression<Func<TagCloud, bool>> filter = tc => tc.TagStatus == true;
            Expression<Func<TagCloud, object>> shorting = tc => tc.TagName;

            dbQueryOptions.filter = filter;
            dbQueryOptions.shorting = shorting;

            List<TagCloud> values = await _repository.GetAllAsync(dbQueryOptions);
            List<GetTagCloudQueryResult> valueToDto = _mapper.Map<List<GetTagCloudQueryResult>>(values);

            return valueToDto;
        }
    }
}
