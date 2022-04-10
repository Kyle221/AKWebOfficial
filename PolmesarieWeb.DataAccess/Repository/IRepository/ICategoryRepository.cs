using PolmesarieWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<ProductCategory>
    {
        void Update(ProductCategory obj);

        void Save();
    }
}
