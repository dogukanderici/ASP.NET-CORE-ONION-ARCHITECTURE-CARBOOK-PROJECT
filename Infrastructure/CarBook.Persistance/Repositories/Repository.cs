using CarBook.Application.Interfaces;
using CarBook.Configurations;
using CarBook.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly CarBookContext _context;

        public Repository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(DbQueryOptions<TEntity> dbQueryOptions)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.includes != null)
            {
                foreach (var include in dbQueryOptions.includes)
                {
                    query = query.Include(include);
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var include in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(include.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(include.Key) == true)
                    {
                        foreach (var thenInclude in dbQueryOptions.thenIncludes[include.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenInclude);
                        }
                    }

                    query = includeQuery;
                }
            }

            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            if (dbQueryOptions.DataTakeNumber != -1)
            {
                query = query.Take(dbQueryOptions.DataTakeNumber);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(DbQueryOptions<TEntity> dbQueryOptions)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.includes != null)
            {
                foreach (var include in dbQueryOptions.includes)
                {
                    query = query.Include(include);
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var include in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(include.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(include.Key) == true)
                    {
                        foreach (var thenInclude in dbQueryOptions.thenIncludes[include.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenInclude);
                        }
                    }

                    query = includeQuery;
                }
            }

            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            if (dbQueryOptions.DataTakeNumber != -1)
            {
                query = query.Take(dbQueryOptions.DataTakeNumber);
            }

            return await query.FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetQueryableEntity(DbQueryOptions<TEntity> dbQueryOptions)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (dbQueryOptions.filter != null)
            {
                query = query.Where(dbQueryOptions.filter);
            }

            if (dbQueryOptions.includes != null)
            {
                foreach (var include in dbQueryOptions.includes)
                {
                    query = query.Include(include);
                }
            }

            if (dbQueryOptions.thenIncludes != null)
            {
                foreach (var include in dbQueryOptions.thenIncludes)
                {
                    var includeQuery = query.Include(include.Key);

                    if (dbQueryOptions.thenIncludes?.ContainsKey(include.Key) == true)
                    {
                        foreach (var thenInclude in dbQueryOptions.thenIncludes[include.Key])
                        {
                            includeQuery = includeQuery.ThenInclude(thenInclude);
                        }
                    }

                    query = includeQuery;
                }
            }

            if (dbQueryOptions.shorting != null)
            {
                if (dbQueryOptions.shortingType == "ascending")
                {
                    query = query.OrderBy(dbQueryOptions.shorting);
                }
                else
                {
                    query = query.OrderByDescending(dbQueryOptions.shorting);
                }
            }

            if (dbQueryOptions.DataTakeNumber != -1)
            {
                query = query.Take(dbQueryOptions.DataTakeNumber);
            }

            return query;
        }

        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
