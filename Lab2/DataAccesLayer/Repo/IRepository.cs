using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repo
{
    public interface IRepository<T>:IDisposable where T:class
    {
        Task<IEnumerable<T>> GetAll();
        void Add(T item);
        void Delete(int id);
        void Update(T item);
        Task<T> GetById(int id);
    }
}
