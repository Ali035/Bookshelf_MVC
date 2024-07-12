using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookshelfWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Try not to use nested types unless there's a clear benefit.
        public enum ActionModeEnum { Create, Update, Delete }
        [ViewData]
        public ActionModeEnum ActionMode { get; set; } = ActionModeEnum.Create;

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Categories.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            ActionMode = ActionModeEnum.Create;
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            ActionMode = ActionModeEnum.Create;
            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Add(category);
                _unitOfWork.Save();

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
            Category? category = _unitOfWork.Categories.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            ActionMode = ActionModeEnum.Update;
            return View("Create", category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            ActionMode = ActionModeEnum.Update;
            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Update(category);
                _unitOfWork.Save();

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
            Category? category = _unitOfWork.Categories.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            ActionMode = ActionModeEnum.Delete;
            return View("Create", category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Category? category = _unitOfWork.Categories.Get(id);
            if (category == null) return NotFound();

            _unitOfWork.Categories.Remove(category);
            _unitOfWork.Save();

            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
    }
}
