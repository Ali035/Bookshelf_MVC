using Bookshelf.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookshelf.DataAccess.Repository.IRepositury
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryList();
    }
}
