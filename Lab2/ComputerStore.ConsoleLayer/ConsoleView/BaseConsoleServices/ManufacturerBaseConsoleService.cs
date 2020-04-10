using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    internal class ManufacturerBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Manufacturer> _crudManufacturerService;
        private readonly IPrintConsoleService _printManufacturerService;

        public ManufacturerBaseConsoleService(ICrudConsoleService<Manufacturer> crudManufacturerService,
            ManufacturerPrintConsoleService printManufacturerService)
        {
            _crudManufacturerService = crudManufacturerService;
            _printManufacturerService = printManufacturerService;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printManufacturerService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudManufacturerService.Add();
                            break;
                        case 2:
                            await _crudManufacturerService.Delete();
                            break;
                        case 3:
                            await _crudManufacturerService.Update();
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
            Console.WriteLine("1. Add manufacturer");
            Console.WriteLine("2. Delete manufacturer");
            Console.WriteLine("3. Update manufacturer");
            Console.WriteLine("4. Back");
        }
    }
}