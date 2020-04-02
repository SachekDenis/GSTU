using System.Collections.Generic;

namespace ComputerStore.BusinessLogicLayer.Managers
{
    public interface IManager<TDto>
    {
        void Add(TDto buyerDto);
        void Delete(int id);
        void Update(TDto buyerDto);
        IEnumerable<TDto> GetAll();
    }
}
