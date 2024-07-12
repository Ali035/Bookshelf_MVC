using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using BookshelfWeb.Controllers.Data;

namespace Bookshelf.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoryRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            this._dbContext = dBContext;
        }

    }
}
