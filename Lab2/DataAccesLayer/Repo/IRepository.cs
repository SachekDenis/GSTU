using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repo
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Delete(int id);
        void Update(T item);
        T GetById(int id);
    }
}
