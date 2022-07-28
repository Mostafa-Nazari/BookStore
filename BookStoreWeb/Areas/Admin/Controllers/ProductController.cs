using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.DataAccess.Repository.UnitOfWork;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Upsert(int? Id)
        {
            ProductViewModel productVM = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() }),
            };

            if (Id is null || Id is 0)
            {
                //Create Product
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == Id);
                return View(productVM);
            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string WWWroot = _webHostEnvironment.WebRootPath;
                if (file is not null)
                {
                    string UID = Guid.NewGuid().ToString();
                    string ProductsImagesRoot = Path.Combine(WWWroot, @"Images\Products");
                    string ImageExtention = Path.GetExtension(file.FileName);
                    if (obj.Product.ImageURL is not null)
                    {
                        string oldImagePath = Path.Combine(ProductsImagesRoot, obj.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (FileStream ImageStream = new FileStream(Path.Combine(ProductsImagesRoot, UID + ImageExtention), FileMode.Create))
                    {
                        file.CopyTo(ImageStream);
                    }
                    obj.Product.ImageURL = UID + ImageExtention;
                }
                if (obj.Product.Id is 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["Success"] = "Prodect Created Successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    TempData["Success"] = "Prodect Updated Successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll("Category,CoverType");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == Id);
            if(obj is null)
            {
                return Json(new { success = false, messaage = "Error While Deleting" });
            }
            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });
        }


        #endregion

    }
}
