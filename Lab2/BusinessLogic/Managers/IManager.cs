using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public interface IManager<TDto>
    {
        void Add(TDto buyerDto);
        void Delete(int id);
        void Update(TDto buyerDto);
        IEnumerable<TDto> GetAll();
    }
}
