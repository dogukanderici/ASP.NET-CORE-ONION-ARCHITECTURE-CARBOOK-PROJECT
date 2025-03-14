using CarBook.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(DbQueryOptions<T> dbQueryOptions);
        Task<T> GetByIdAsync(DbQueryOptions<T> dbQueryOptions);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        IQueryable<T> GetQueryableEntity(DbQueryOptions<T> dbQueryOptions);
    }
}
