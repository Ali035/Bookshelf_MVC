using Bookshelf.DataAccess.Repository.IRepositury;
using BookshelfWeb.Controllers.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookshelf.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<TEntity> _dbSet;

        public Repository(ApplicationDBContext db)
        {
            this._dbContext = db;
            this._dbSet = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public TEntity? Get(object id)
        {
            return _dbSet.Find(id);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll(string[]? includeParams)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includeParams != null && includeParams.Length > 0)
            {
                query = includeParams.Aggregate(query, (current, include) => current.Include(include));
            }
            return query.ToList();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}
