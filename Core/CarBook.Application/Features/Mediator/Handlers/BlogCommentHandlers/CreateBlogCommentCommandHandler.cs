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
    public class CreateBlogCommentCommandHandler : IRequestHandler<CreateBlogCommentCommand>
    {
        private readonly IRepository<BlogComment> _repository;
        private readonly IMapper _mapper;

        public CreateBlogCommentCommandHandler(IRepository<BlogComment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateBlogCommentCommand request, CancellationToken cancellationToken)
        {
            BlogComment valueFromDto = _mapper.Map<BlogComment>(request);

            await _repository.CreateAsync(valueFromDto);
        }
    }
}
