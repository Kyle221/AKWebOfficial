using PolmesarieWeb.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Brand = new BrandRepository(_db);
            Product = new ProductRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public IBrandRepository Brand { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
