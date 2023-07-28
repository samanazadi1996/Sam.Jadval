using Jadval.Application.Interfaces.Repositories;
using Jadval.Application.Parameters;
using Jadval.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Jadval.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        protected IQueryable<T> Paged(IQueryable<T> query, PagenationRequestParameter requestParameter)
        {
            var columnName = requestParameter.Order ?? "Created";

            query = OrderByColumn(query);

            query = query
                .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                .Take(requestParameter.PageSize);

            return query;
            IQueryable<T> OrderByColumn(IQueryable<T> collection)
            {

                PropertyInfo prop = typeof(T).GetProperty(columnName);

                ParameterExpression param = Expression.Parameter(typeof(T), "x");

                MemberExpression propertyExpression = Expression.Property(param, prop);

                LambdaExpression lambda = Expression.Lambda(propertyExpression, param);

                string methodName = requestParameter.Desc ? "OrderByDescending" : "OrderBy";

                MethodCallExpression orderByExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), prop.PropertyType },
                    collection.Expression,
                    Expression.Quote(lambda)
                );

                return collection.Provider.CreateQuery<T>(orderByExpression);

            }
        }
    }
}
