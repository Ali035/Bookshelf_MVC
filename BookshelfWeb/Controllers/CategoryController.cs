using BookshelfWeb.Controllers.Data;
using BookshelfWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookshelfWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext dbContext) => _dbContext = dbContext;
        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }

    }
}
