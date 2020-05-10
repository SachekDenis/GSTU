using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    public class SupplierBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Supplier> _crudSupplierService;
        private readonly IPrintConsoleService _printSupplierService;

        public SupplierBaseConsoleService(ICrudConsoleService<Supplier> crudSupplierService, SupplierPrintConsoleService printSupplierService)
        {
            _crudSupplierService = crudSupplierService;
            _printSupplierService = printSupplierService;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printSupplierService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudSupplierService.Add();
                            break;
                        case 2:
                            await _crudSupplierService.Delete();
                            break;
                        case 3:
                            await _crudSupplierService.Update();
                            break;
                        case 4:
                            return;
                    }
                }
                catch (ValidationException e)
                {
                    Console.WriteLine($"Validation error. Message: {e.Message}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Add supplier");
            Console.WriteLine("2. Delete supplier");
            Console.WriteLine("3. Update supplier");
            Console.WriteLine("4. Back");
        }
    }
}