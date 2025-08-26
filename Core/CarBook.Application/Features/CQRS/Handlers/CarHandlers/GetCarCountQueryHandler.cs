using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarHandlers
{
    public class GetCarCountQueryHandler
    {
        private readonly IRepository<Car> _repository;

        public GetCarCountQueryHandler(IRepository<Car> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle()
        {
            DbQueryOptions<Car> dbQueryOptions = new DbQueryOptions<Car>();

            int dataCount = await _repository.GetDataCount(dbQueryOptions);

            return dataCount;
        }
    }
}
