using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PolmesarieWeb.DataAccess;
using PolmesarieWeb.DataAccess.Repository.IRepository;
using PolmesarieWeb.Models;
using PolmesarieWeb.Models.ViewModels;

namespace PolmesarieWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            //IList<Brand> objList = _unitOfWork.Brand.GetAll();
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            //Product product = new();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text=u.CategoryName,
            //        Value= u.Id.ToString()
            //    });

            //IEnumerable<SelectListItem> BrandsList = _unitOfWork.Brand.GetAll().Select(
            //   u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.Id.ToString()
            //   });


            ProductVM productVM = new()
            { 
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i=> new SelectListItem
                {
                    Text= i.CategoryName,
                    Value= i.Id.ToString()
                }),
                BrandsList = _unitOfWork.Brand.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id== null || id == 0)
            {
                //Create the product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["BrandsList"] = BrandsList;
                return View(productVM);
            }
            else
            {
                //update the product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //protect against anti forgery token attacks.
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            //if(obj.CategoryName == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("CustomError", "Display Order cannot match the Name");
            //}
            if(ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images/products");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.Product.PictureUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.PictureUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.PictureUrl = @"/images/products/" + fileName + extension;
                }
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
       
        }


        

        ////GET
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //var categoryFromDb = _db.ProductCategories.Find(id);
        //    var objFromDbFirst = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
        //    if (objFromDbFirst == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(objFromDbFirst);
        //}

        

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "ProductCategory,Brand");
            return Json(new {data = productList});
        }
        [HttpDelete]
        
        public IActionResult Delete(int? id)
        {

            //var obj = _db.ProductCategories.Find(id);

            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "An error occured while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.PictureUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Deleted Successfully" });


        }
        #endregion

    }

}

