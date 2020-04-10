using System.Threading.Tasks;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public interface ICrudConsoleService<T>
    {
        Task Add();
        Task Delete();
        Task Update();
        Task<T> CreateModel();
    }
}