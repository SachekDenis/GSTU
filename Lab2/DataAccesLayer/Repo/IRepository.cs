using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repo
{
    public interface IRepository<T>:IDisposable where T:class
    {
        IQueryable<T> GetAll();
        Task Add(T item);
        Task Delete(int id);
        Task Update(T item);
        Task<T> GetById(int id);
    }
}
