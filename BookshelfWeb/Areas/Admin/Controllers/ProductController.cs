using Bookshelf.DataAccess.Repository.IRepositury;
using Bookshelf.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookshelfWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(
                [nameof(Product.Category)]).ToList();
            return Json(products);
        }
    }
}
