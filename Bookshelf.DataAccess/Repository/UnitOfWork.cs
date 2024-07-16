using Bookshelf.DataAccess.Repository.IRepositury;
using BookshelfWeb.DataAccess.Data;

namespace Bookshelf.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            Categories = new CategoryRepository(dBContext);
            Product = new ProductRepository(dBContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
