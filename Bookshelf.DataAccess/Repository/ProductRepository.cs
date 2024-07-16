using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using BookshelfWeb.DataAccess.Data;

namespace Bookshelf.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ProductRepository(ApplicationDBContext dBContext)
            : base(dBContext)
        {
            this._dbContext = dBContext;
        }
    }
}
