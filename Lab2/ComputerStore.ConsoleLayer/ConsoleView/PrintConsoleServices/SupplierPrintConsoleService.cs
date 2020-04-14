using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;

namespace ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices
{
    public class SupplierPrintConsoleService : IPrintConsoleService
    {
        private readonly SupplierManager _supplierManager;

        public SupplierPrintConsoleService(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        public async Task PrintAll()
        {
            var items = await _supplierManager.GetAll();
            items.WriteCollectionAsTable();
        }
    }
}