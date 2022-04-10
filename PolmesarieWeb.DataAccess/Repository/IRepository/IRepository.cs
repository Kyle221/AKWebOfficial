using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PolmesarieWeb.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IList<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IList<T> entity);

    }
}
