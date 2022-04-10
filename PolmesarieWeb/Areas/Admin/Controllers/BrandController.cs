using Microsoft.AspNetCore.Mvc;
using PolmesarieWeb.DataAccess;
using PolmesarieWeb.DataAccess.Repository.IRepository;
using PolmesarieWeb.Models;

namespace PolmesarieWeb.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IList<Brand> objList = _unitOfWork.Brand.GetAll();
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
            var objFromDbFirst = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
            if(objFromDbFirst == null)
            {
                return NotFound();
            }

            return View(objFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //protect against anti forgery token attacks.
        public IActionResult Edit(Brand obj)
        {
            //if(obj.CategoryName == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CustomError", "Display Order cannot match the Name");
            //}
            if(ModelState.IsValid)
            {
                _unitOfWork.Brand.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Brand Updated Successfully!";
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
        public IActionResult Create(Brand obj)
        {
            //if(obj.CategoryName == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CustomError", "Display Order cannot match the Name");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Brand.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Brand Added Successfully!";
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
            var objFromDbFirst = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
            if (objFromDbFirst == null)
            {
                return NotFound();
            }

            return View(objFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //protect against anti forgery token attacks.
        public IActionResult DeletePOST(int? id)
        {

            //var obj = _db.ProductCategories.Find(id);

            var obj = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
           if(obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Brand.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Brand Removed Successfully!";
            return RedirectToAction("Index");

        }

    }
}
