using System.Linq.Expressions;

namespace Bookshelf.DataAccess.Repository.IRepositury
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(string[]? includeParams = null);
        TEntity? Get(object id);
        TEntity? Get(Expression<Func<TEntity, bool>> filter, string[]? includeParams = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
