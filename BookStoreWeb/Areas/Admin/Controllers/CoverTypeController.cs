using BookStore.DataAccess.Repository.UnitOfWork;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> CoverTypeTableRows = _unitOfWork.CoverType.GetAll();
            return View(CoverTypeTableRows);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created Successfully";
                return RedirectToAction("Index", "CoverType");
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
            var CoverTypeById = _unitOfWork.CoverType.Get(u=>u.Id==Id);
            if (CoverTypeById == null)
            {
                return NotFound();
            }

            return View(CoverTypeById);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Edited successfully";
                return RedirectToAction("Index", "CoverType");
            }

            return View(obj);

        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var CoverTypeById = _unitOfWork.CoverType.Get(u => u.Id == Id);
            if (CoverTypeById == null)
            {
                return NotFound();
            }

            return View(CoverTypeById);
        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoverType coverType = _unitOfWork.CoverType.Get(u => u.Id == id);
            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
