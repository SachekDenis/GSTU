using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;

namespace ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices
{
    public class ManufacturerPrintConsoleService : IPrintConsoleService
    {
        private readonly ManufacturerManager _manufacturerManager;

        public ManufacturerPrintConsoleService(ManufacturerManager manufacturerManager)
        {
            _manufacturerManager = manufacturerManager;
        }

        public async Task PrintAll()
        {
            var items = await _manufacturerManager.GetAll();
            items.WriteCollectionAsTable();
        }
    }
}