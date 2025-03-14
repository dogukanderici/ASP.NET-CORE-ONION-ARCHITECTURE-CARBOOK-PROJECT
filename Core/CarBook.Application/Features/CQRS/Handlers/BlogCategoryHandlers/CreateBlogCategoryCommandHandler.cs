using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands;
using CarBook.Application.Interfaces;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BlogCategoryHandlers
{
    public class CreateBlogCategoryCommandHandler
    {
        private readonly IRepository<BlogCategory> _repository;
        private readonly IMapper _mapper;

        public CreateBlogCategoryCommandHandler(IRepository<BlogCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateBlogCategoryCommand createBlogCategoryCommand)
        {
            BlogCategory valueFromDto = _mapper.Map<BlogCategory>(createBlogCategoryCommand);

            await _repository.CreateAsync(valueFromDto);
        }
    }
}
