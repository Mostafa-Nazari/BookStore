using BookStore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess.Repository.UnitOfWork;
using BookStore.Models.ViewModels;

namespace BookStore.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int? ID)
        {
            ShoppingCart cartObj = new ShoppingCart()
            {
                Product = _unitOfWork.Product.
                        Get(u => u.Id == ID, includeProperties: "Category,CoverType"),
                Count = 1,
            };
            return View(cartObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}