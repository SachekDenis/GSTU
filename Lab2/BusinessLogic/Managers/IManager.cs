using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public interface IManager<TDto>
    {
        Task Add(TDto buyerDto);
        Task Delete(int id);
        Task Update(TDto buyerDto);
        IEnumerable<TDto> GetAll();
    }
}
