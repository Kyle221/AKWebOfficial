using PolmesarieWeb.DataAccess.Repository.IRepository;
using PolmesarieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.DataAccess.Repository
{
    public class CategoryRepository : Repository<ProductCategory>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ProductCategory obj)
        {
            _db.ProductCategories.Update(obj);
        }
    }
}
