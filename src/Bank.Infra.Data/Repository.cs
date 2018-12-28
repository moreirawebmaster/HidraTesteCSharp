using Bank.Commun.Domain.Entitie;
using Bank.Infra.CrossCutting.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bank.Infra.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IDbContext _context;

        public IQueryable<TEntity> Query => _context.Set<TEntity>().AsQueryable();

        public Repository(IDbContext context) => _context = context;

        public Task<TEntity> GetByIdAsync(object id) => GetByIdAsync(id, null);

        public Task<TEntity> GetByIdAsync(object id, params Expression<Func<TEntity, object>>[] includes)
        {

            var entity = _context.Set<TEntity>();
            if (includes?.Count() > 0)
                entity = includes.Aggregate(entity, (current, include) => current.Include(include) as DbSet<TEntity>);

            return entity.FindAsync(id);
        }

        public Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> by) => GetByAsync(by, null);

        public Task<List<TEntity>> GetByAsync(params Expression<Func<TEntity, object>>[] includes) => GetByAsync(null, includes);

        public async Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> by, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query;

            if (includes?.Count() > 0)
                query = includes.Aggregate(query, (current, include) => current.Include(include));


            if (by != null)
                query = query.Where(by);

            var data = await query.AsNoTracking().ToListAsync();
            return data;
        }

        public Task<TEntity> GetByOneAsync(Expression<Func<TEntity, bool>> by) => GetByOneAsync(by, null);

        public async Task<TEntity> GetByOneAsync(Expression<Func<TEntity, bool>> by, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query;

            if (includes?.Count() > 0)
                query = includes.Aggregate(query, (current, include) => current.Include(include));


            var data = await query.AsNoTracking().FirstOrDefaultAsync(by);
            return data;

        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task RangeInsertAsync(List<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task BulkUpdateAsync(List<TEntity> entities)
        {

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    _context.Entry(entity);

            return Task.FromResult(_context);
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.FromResult(_context.Entry(entity));
        }

        public Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.FromResult(_context.Set<TEntity>().Remove(entity));

        }

        public Task<int> SaveChangeAsync() => _context.SaveChangeAsync();
    }
}