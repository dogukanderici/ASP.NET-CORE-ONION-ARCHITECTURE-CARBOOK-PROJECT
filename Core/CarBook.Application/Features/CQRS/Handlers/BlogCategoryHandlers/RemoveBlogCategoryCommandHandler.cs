using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.BlogCategoryCommands;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.BlogCategoryHandlers
{
    public class RemoveBlogCategoryCommandHandler
    {
        private readonly IRepository<BlogCategory> _repository;

        public RemoveBlogCategoryCommandHandler(IRepository<BlogCategory> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBlogCategoryCommand removeBlogCategoryCommand)
        {
            DbQueryOptions<BlogCategory> dbQueryOptions = new DbQueryOptions<BlogCategory>();

            Expression<Func<BlogCategory, bool>> filter = x => x.BlogCategoryID == removeBlogCategoryCommand.Id;

            dbQueryOptions.filter = filter;

            BlogCategory value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
