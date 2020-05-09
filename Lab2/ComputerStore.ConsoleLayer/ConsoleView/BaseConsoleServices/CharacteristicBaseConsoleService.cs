using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    public class CharacteristicBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Characteristic> _crudCharacteristicService;
        private readonly IPrintConsoleService _printCharacteristicService;

        public CharacteristicBaseConsoleService(ICrudConsoleService<Characteristic> crudCharacteristicService,
                                                CharacteristicPrintConsoleService printCharacteristicService)
        {
            _crudCharacteristicService = crudCharacteristicService;
            _printCharacteristicService = printCharacteristicService;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printCharacteristicService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await _crudCharacteristicService.Add();
                            break;
                        case 2:
                            await _crudCharacteristicService.Delete();
                            break;
                        case 3:
                            await _crudCharacteristicService.Update();
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
            Console.WriteLine("1. Add characteristic");
            Console.WriteLine("2. Delete characteristic");
            Console.WriteLine("3. Update characteristic");
            Console.WriteLine("4. Back");
        }
    }
}