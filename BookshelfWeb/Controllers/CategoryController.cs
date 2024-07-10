using BookshelfWeb.Controllers.Data;
using BookshelfWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookshelfWeb.Controllers
{
    public class CategoryController(ApplicationDBContext dbContext) : Controller
    {
        private readonly ApplicationDBContext _dbContext = dbContext;

        public async Task<IActionResult> Index()
        {
            List<Category> objCategoryList = await _dbContext.Categories.ToListAsync();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
