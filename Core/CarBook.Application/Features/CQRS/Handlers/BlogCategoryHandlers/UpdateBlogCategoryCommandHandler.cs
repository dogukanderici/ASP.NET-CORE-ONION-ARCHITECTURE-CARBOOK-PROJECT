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
    public class UpdateBlogCategoryCommandHandler
    {
        private readonly IRepository<BlogCategory> _repository;
        private readonly IMapper _mapper;

        public UpdateBlogCategoryCommandHandler(IRepository<BlogCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBlogCategoryCommand updateBlogCategoryCommand)
        {
            BlogCategory valueFromDto = _mapper.Map<BlogCategory>(updateBlogCategoryCommand);

            await _repository.UpdateAsync(valueFromDto);
        }
    }
}
