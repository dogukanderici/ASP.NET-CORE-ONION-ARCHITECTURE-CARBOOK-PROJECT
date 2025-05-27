using AutoMapper;
using CarBook.Application.Features.CQRS.Commands.CarReviewCommands;
using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarReviewHandlers
{
    public class RemoveCarReviewCommandHandler
    {
        private readonly IRepository<CarReview> _repository;

        public RemoveCarReviewCommandHandler(IRepository<CarReview> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveCarReviewCommand removeCarReviewCommand)
        {
            DbQueryOptions<CarReview> dbQueryOptions = new DbQueryOptions<CarReview>();

            Expression<Func<CarReview, bool>> filter = x => x.CarReviewID == removeCarReviewCommand.CarReviewID;

            dbQueryOptions.filter = filter;

            CarReview value = await _repository.GetByIdAsync(dbQueryOptions);

            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }

        }
    }
}
