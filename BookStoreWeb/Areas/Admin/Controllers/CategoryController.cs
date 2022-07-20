using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess.Repository.UnitOfWork;


namespace BookStore.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> CategoryTableRows = _unitOfWork.Category.GetAll();
            return View(CategoryTableRows);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }

            return View(obj);

        }

        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var CategoryById = _unitOfWork.Category.Get(u=>u.Id == Id);
            if (CategoryById == null)
            {
                return NotFound();
            }

            return View(CategoryById);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index", "Category");
            }

            return View(obj);

        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var CategoryById = _unitOfWork.Category.Get(u=>u.Id==Id);
            if (CategoryById == null)
            {
                return NotFound();
            }

            return View(CategoryById);
        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _unitOfWork.Category.Get(u=>u.Id==id);
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
