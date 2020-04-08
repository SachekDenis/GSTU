namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public abstract class CrudConsoleService<T> : BaseConsoleService
    {
        protected abstract void Add();
        protected abstract void Delete();
        protected abstract void Update();
        protected abstract T CreateModel();
    }
}