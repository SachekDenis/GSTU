using ComputerStore.DataAccessLayer.Models;

namespace ComputerStore.BusinessLogicLayer.Validation
{
    public abstract class Validator<T>
        where T : class, IEntity
    {
        public abstract bool Validate(T item);
    }
}