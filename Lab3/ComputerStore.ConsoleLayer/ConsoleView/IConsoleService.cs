using System.Threading.Tasks;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public interface IConsoleService
    {
        Task StartConsoleLoop();
        void PrintMenu();
    }
}