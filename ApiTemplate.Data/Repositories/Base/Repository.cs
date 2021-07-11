using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RabitMQTask.Common.Exceptions;
using RabitMQTask.Core.Entities.Base;
using RabitMQTask.Data.ApplicationDbContexts;

namespace RabitMQTask.Data.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        #region Fields

        private readonly AppDbContext _appDbContext;
        protected DbSet<T> Entities => _appDbContext.Set<T>();
        public IQueryable<T> Table => Entities.AsQueryable();
        public IQueryable<T> TableAsNoTracking => Entities.AsNoTracking();
        #endregion
        #region Constructors
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion

        #region Methods

        #region Get
        public async Task<T> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false,
 CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TResult> Get<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<TResult>> GetAll<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var query = asNoTracking ? TableAsNoTracking : Table;
            if (!(include is null))
            {
                query = include(query);
            }

            if (!(expression is null))
            {
                query = query.Where(expression);
            }

            if (!(orderBy is null))
            {
                query = orderBy(query);
            }

            return await query.Select(selector).ToListAsync(cancellationToken);
        }

        public async Task<T> GetById(object id, CancellationToken cancellationToken = default)
        {
            if (id is null)
            {
                throw new NotFoundException($"the {nameof(id)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            return await Entities.FindAsync(id, cancellationToken);
        }

        #endregion

        #region Insert
        public async Task Insert(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            await Entities.AddAsync(entity, cancellationToken);
        }

        public async Task Insert(List<T> entities, CancellationToken cancellationToken = default)
        {
            if (entities is null)
            {
                throw new NotFoundException($"the {nameof(entities)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            await Entities.AddRangeAsync(entities, cancellationToken);
        }

        #endregion

        #region Delete
        public async Task Delete(object id, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }

            Entities.Remove(entity);
        }

        public void Delete(T entity)
        {
            if (entity is null)
            {
                throw new NotFoundException($"the {nameof(entity)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            Entities.Remove(entity);
        }

        public void Delete(List<T> entities)
        {
            if (entities is null)
            {
                throw new NotFoundException($"the {nameof(entities)} not found in {GetType().Name}/{MethodBase.GetCurrentMethod().Name}");
            }
            Entities.RemoveRange(entities);
        }
        #endregion


        #endregion

    }
}