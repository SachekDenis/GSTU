namespace ComputerStore.BusinessLogicLayer.Validation
{
    public interface IValidator<T> where T : class
    {
        bool Validate(T item);
    }
}