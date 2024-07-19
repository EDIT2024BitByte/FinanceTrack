using Comtrade.FinanceTrack.Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void DeleteAll(IEnumerable<TEntity> entitiesToDelete);
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IEnumerable<TEntity> GetAsNoTracking(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        Task<TEntity> GetByIDAsync(object id);
        TEntity Insert(TEntity entity);
        Task<bool> Insert(ICollection<TEntity> entity);
        void Update(TEntity entityToUpdate);
        Task<long> CountTotal(List<Expression<Func<TEntity, bool>>> filteri);
        Task<List<TEntity>> GetEntitiesPaginatedAsync(List<Expression<Func<TEntity, bool>>> filteri, string includeProperties = "", PagedQuery paginator = null);
    }
}
