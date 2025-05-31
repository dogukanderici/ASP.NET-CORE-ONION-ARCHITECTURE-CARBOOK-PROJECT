using CarBook.Application.Features.Mediator.Queries.AppUserQueries;
using CarBook.Application.Features.Mediator.Results.AppUserResults;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CarBook.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetLoginAppUserQueryHandler : IRequestHandler<GetLoginAppUserQuery, GetLoginAppUserQueryResult>
    {
        public Task<GetLoginAppUserQueryResult> Handle(GetLoginAppUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
