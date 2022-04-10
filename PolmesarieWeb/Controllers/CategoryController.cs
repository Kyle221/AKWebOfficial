using Microsoft.AspNetCore.Mvc;
using PolmesarieWeb.DataAccess;
using PolmesarieWeb.DataAccess.Repository.IRepository;
using PolmesarieWeb.Models;

namespace PolmesarieWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IList<ProductCategory> objList = _db.GetAll();
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
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
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
                _db.Update(obj);
                _db.Save();
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
                _db.Add(obj);
                _db.Save();
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
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
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

            var obj = _db.GetFirstOrDefault(u => u.Id == id);
           if(obj == null)
            {
                return NotFound();
            }
           _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Product Category Removed Successfully!";
            return RedirectToAction("Index");

        }

    }
}
