using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public interface IManager<T>
    {
        Task Add(T buyerDto);
        Task Delete(int id);
        Task Update(T buyerDto);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}