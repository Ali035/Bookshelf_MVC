using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using BookshelfWeb.Controllers.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookshelf.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoryRepository(ApplicationDBContext dBContext)
            : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public IEnumerable<SelectListItem> GetCategoryList()
        {
            return GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
        }
    }
}
