using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using Bookshelf.Models.ViewModel;
using Bookshelf.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                new() { Product = new Product(), CategoryList = GetCategoryList() };
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
            ProductVM productVM = new() { Product = product, CategoryList = GetCategoryList(), };
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
                        string oldImagePath = Path.Combine(
                            filePath.WebRootPath,
                            productVM.Product.ImageUrl.TrimStart('\\')
                        );
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
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
            productVM.CategoryList = GetCategoryList();
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
            productVM.CategoryList = GetCategoryList();
            return View(productVM);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = UnitOfWork
                .Product.GetAll([nameof(Product.Category)])
                .ToList();
            return Json(products);
        }

        private IEnumerable<SelectListItem> GetCategoryList()
        {
            return UnitOfWork
                .Categories.GetAll()
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
        }
    }
}
