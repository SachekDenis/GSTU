namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public abstract class BaseConsoleService : IConsoleService
    {
        public abstract void StartConsoleLoop();
        protected abstract void PrintAll();
        protected abstract void PrintMenu();
    }
}