using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.DataAccessLayer.Repo
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task Add(T item);
        Task Delete(int id);
        Task Update(T item);
        Task<T> GetById(int id);
    }
}