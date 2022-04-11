using PolmesarieWeb.DataAccess.Repository.IRepository;
using PolmesarieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.CustomerPrice = obj.CustomerPrice;
                objFromDb.ResellerPrice = obj.ResellerPrice;
                objFromDb.ProductCategoryId = obj.ProductCategoryId;
                objFromDb.BrandId = obj.BrandId;

                //Image update code
                if(obj.PictureUrl != null)
                {
                    objFromDb.PictureUrl = obj.PictureUrl;
                }
                
            }
        }
    }
}
