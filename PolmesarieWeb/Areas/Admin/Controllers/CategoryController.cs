using Microsoft.AspNetCore.Mvc;
using PolmesarieWeb.DataAccess;
using PolmesarieWeb.DataAccess.Repository.IRepository;
using PolmesarieWeb.Models;

namespace PolmesarieWeb.Controllers
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
            IList<ProductCategory> objList = _unitOfWork.Category.GetAll();
            return View(objList);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.ProductCategories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if(categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //protect against anti forgery token attacks.
        public IActionResult Edit(ProductCategory obj)
        {
            //if(obj.CategoryName == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CustomError", "Display Order cannot match the Name");
            //}
            if(ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Category Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
       
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //protect against anti forgery token attacks.
        public IActionResult Create(ProductCategory obj)
        {
            //if(obj.CategoryName == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CustomError", "Display Order cannot match the Name");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Category Added Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.ProductCategories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //protect against anti forgery token attacks.
        public IActionResult DeletePOST(int? id)
        {

            //var obj = _db.ProductCategories.Find(id);

            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
           if(obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Category Removed Successfully!";
            return RedirectToAction("Index");

        }

    }
}
