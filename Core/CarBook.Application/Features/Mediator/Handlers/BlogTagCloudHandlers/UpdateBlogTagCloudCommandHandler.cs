using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogTagCloudCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.BlogTagCloudHandlers
{
    public class UpdateBlogTagCloudCommandHandler : IRequestHandler<UpdateBlogTagCloudCommand>
    {
        private readonly IRepository<BlogTagCloud> _repository;
        private readonly IMapper _mapper;

        public UpdateBlogTagCloudCommandHandler(IRepository<BlogTagCloud> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBlogTagCloudCommand request, CancellationToken cancellationToken)
        {
            BlogTagCloud valueFromDto = _mapper.Map<BlogTagCloud>(request);

            await _repository.UpdateAsync(valueFromDto);
        }
    }
}
