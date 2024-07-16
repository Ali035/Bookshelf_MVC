using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using Bookshelf.Models.ViewModel;
using Bookshelf.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BookshelfWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        : Controller
    {
        [ViewData]
        public ActionModeEnum ActionMode { get; set; } = ActionModeEnum.Create;

        private IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
        private IWebHostEnvironment WebHostEnvironment { get; set; } = webHostEnvironment;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ProductVM productVM =
                new()
                {
                    Product = new Product(),
                    CategoryList = UnitOfWork.Categories.GetCategoryList()
                };
            return View(productVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Product? product = UnitOfWork.Product.Get(id);
            if (product == null || product.Id == 0)
            {
                return NotFound();
            }
            ProductVM productVM =
                new()
                {
                    Product = product,
                    CategoryList = UnitOfWork.Categories.GetCategoryList(),
                };
            ActionMode = ActionModeEnum.Update;
            return View("Create", productVM);
        }

        [HttpPost]
        public IActionResult Edit(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    FilePath filePath =
                        new(WebHostEnvironment.WebRootPath, nameof(Product), file.FileName);

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        FileManager fileManager = new(WebHostEnvironment.WebRootPath);
                        fileManager.RemoveFileIfExists(productVM.Product.ImageUrl);
                    }

                    using var fs = new FileStream(filePath.FileStreamPath, FileMode.Create);
                    file.CopyTo(fs);
                    productVM.Product.ImageUrl = filePath.Url;
                }
                UnitOfWork.Product.Update(productVM.Product);
                UnitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return View("Index");
            }
            productVM.CategoryList = UnitOfWork.Categories.GetCategoryList();
            ActionMode = ActionModeEnum.Update;
            return View("Create", productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    FilePath filePath =
                        new(WebHostEnvironment.WebRootPath, nameof(Product), file.FileName);
                    using var fs = new FileStream(filePath.FileStreamPath, FileMode.Create);
                    file.CopyTo(fs);
                    productVM.Product.ImageUrl = filePath.Url;
                }
                UnitOfWork.Product.Add(productVM.Product);
                UnitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return View("Index");
            }
            productVM.CategoryList = UnitOfWork.Categories.GetCategoryList();
            return View(productVM);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Product Not Found!" });
            }
            Product? product = UnitOfWork.Product.Get(id);
            if (product == null)
            {
                return Json(new ResultMessage(true, "Product Not Found!"));
            }
            FileManager fileManager = new(WebHostEnvironment.WebRootPath);
            if (product.ImageUrl != null || product.ImageUrl != null)
            {
                fileManager.RemoveFileIfExists(product.ImageUrl);
            }
            UnitOfWork.Product.Remove(product);
            UnitOfWork.Save();
            return Json(new ResultMessage(true, "Product deleted successfully."));
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = UnitOfWork
                .Product.GetAll([nameof(Product.Category)])
                .ToList();
            return Json(products);
        }

        #endregion
    }
}
