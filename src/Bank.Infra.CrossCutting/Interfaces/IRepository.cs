using Bank.Commun.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bank.Infra.CrossCutting.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Query { get; }
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetByIdAsync(object id, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> by);
        Task<List<TEntity>> GetByAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> by, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByOneAsync(Expression<Func<TEntity, bool>> by);
        Task<TEntity> GetByOneAsync(Expression<Func<TEntity, bool>> by, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> InsertAsync(TEntity entity);
        Task RangeInsertAsync(List<TEntity> entities);
        Task BulkUpdateAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<int> SaveChangeAsync();
    }
}