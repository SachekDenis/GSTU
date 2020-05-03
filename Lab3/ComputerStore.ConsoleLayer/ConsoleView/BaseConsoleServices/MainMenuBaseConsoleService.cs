using System;
using System.Threading.Tasks;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    internal class MainMenuBaseConsoleService : IConsoleService
    {
        private readonly CategoryBaseConsoleService _categoryConsoleService;
        private readonly CharacteristicBaseConsoleService _characteristicConsoleService;
        private readonly ManufacturerBaseConsoleService _manufacturerConsoleService;
        private readonly OrderBaseConsoleService _orderConsoleService;
        private readonly ProductListBaseConsoleService _productListConsoleService;

        public MainMenuBaseConsoleService(ManufacturerBaseConsoleService manufacturerConsoleService,
                                          CategoryBaseConsoleService categoryConsoleService,
                                          CharacteristicBaseConsoleService characteristicConsoleService,
                                          ProductListBaseConsoleService productListConsoleService,
                                          OrderBaseConsoleService orderConsoleService)
        {
            _manufacturerConsoleService = manufacturerConsoleService;
            _categoryConsoleService = categoryConsoleService;
            _productListConsoleService = productListConsoleService;
            _orderConsoleService = orderConsoleService;
            _characteristicConsoleService = characteristicConsoleService;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    PrintMenu();
                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                        {
                            await _manufacturerConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 2:
                        {
                            await _characteristicConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 3:
                        {
                            await _categoryConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 4:
                        {
                            await _productListConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 5:
                        {
                            await _orderConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 6:
                        {
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Manufacturers");
            Console.WriteLine("2. Characteristics");
            Console.WriteLine("3. Categories");
            Console.WriteLine("4. Products");
            Console.WriteLine("5. Orders");
            Console.WriteLine("6. Exit");
        }
    }
}