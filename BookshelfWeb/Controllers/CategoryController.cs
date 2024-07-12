using Bookshelf.Models;
using BookshelfWeb.Controllers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookshelfWeb.Controllers
{
    public class CategoryController(ApplicationDBContext dbContext) : Controller
    {
        private readonly ApplicationDBContext _dbContext = dbContext;

        // Try not to use nested types unless there's a clear benefit.
        public enum ActionModeEnum { Create, Update, Delete }
        [ViewData]
        public ActionModeEnum ActionMode { get; set; } = ActionModeEnum.Create;

        public async Task<IActionResult> Index()
        {
            List<Category> objCategoryList = await _dbContext.Categories.ToListAsync();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            ActionMode = ActionModeEnum.Create;
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            ActionMode = ActionModeEnum.Create;
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();

                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View("Create");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            ActionMode = ActionModeEnum.Update;
            return View("Create", category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            ActionMode = ActionModeEnum.Update;
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();

                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            ActionMode = ActionModeEnum.Delete;
            return View("Create", category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Category? category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
    }
}
