using AutoMapper;
using CarBook.Application.Features.Mediator.Queries.SocialMediaQueries;
using CarBook.Application.Features.Mediator.Results.SocialMediaResults;
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

namespace CarBook.Application.Features.Mediator.Handlers.SocialMediaHandlers
{
    public class GetSocialMediaByIdQueryHandler : IRequestHandler<GetSocialMediaByIdQuery, GetSocialMediaByIdQueryResult>
    {
        private readonly IRepository<SocialMedia> _repository;
        private readonly IMapper _mapper;

        public GetSocialMediaByIdQueryHandler(IRepository<SocialMedia> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetSocialMediaByIdQueryResult> Handle(GetSocialMediaByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<SocialMedia> dbQueryOptions = new DbQueryOptions<SocialMedia>();

            Expression<Func<SocialMedia, bool>> filter = x => x.SocialMediaID == request.Id;

            dbQueryOptions.filter = filter;

            SocialMedia value = await _repository.GetByIdAsync(dbQueryOptions);

            GetSocialMediaByIdQueryResult valueToDto = _mapper.Map<GetSocialMediaByIdQueryResult>(value);

            return valueToDto;
        }
    }
}
