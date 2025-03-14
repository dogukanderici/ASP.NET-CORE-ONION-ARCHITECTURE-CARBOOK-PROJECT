using AutoMapper;
using CarBook.Application.Features.Mediator.Commands.BlogCommentCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Mediator.Handlers.BlogCommentHandlers
{
    public class UpdateBlogCommentCommandHandler : IRequestHandler<UpdateBlogCommentCommand>
    {
        private readonly IRepository<BlogComment> _repository;
        private readonly IMapper _mapper;

        public UpdateBlogCommentCommandHandler(IRepository<BlogComment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBlogCommentCommand request, CancellationToken cancellationToken)
        {
            BlogComment valueFromDto = _mapper.Map<BlogComment>(request);

            await _repository.UpdateAsync(valueFromDto);
        }
    }
}
