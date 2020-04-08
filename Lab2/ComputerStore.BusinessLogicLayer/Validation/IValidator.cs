using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public interface IValidator<T>
        where T : class, IEntity
    {
        bool Validate(T item);
    }
}