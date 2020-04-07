using System;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    internal class MainMenuConsoleService:IConsoleService
    {
        private readonly CategoryConsoleService _categoryConsoleService;
        private readonly CharacteristicConsoleService _characteristicConsoleService;
        private readonly ManufacturerConsoleService _manufacturerConsoleService;
        private readonly OrderConsoleService _orderConsoleService;
        private readonly ProductListConsoleService _productListConsoleService;
        private readonly SupplierConsoleService _supplierConsoleService;

        public MainMenuConsoleService(ManufacturerConsoleService manufacturerConsoleService,
            SupplierConsoleService supplierConsoleService,
            CategoryConsoleService categoryConsoleService,
            CharacteristicConsoleService characteristicConsoleService,
            ProductListConsoleService productListConsoleService,
            OrderConsoleService orderConsoleService)
        {
            _manufacturerConsoleService = manufacturerConsoleService;
            _supplierConsoleService = supplierConsoleService;
            _categoryConsoleService = categoryConsoleService;
            _productListConsoleService = productListConsoleService;
            _orderConsoleService = orderConsoleService;
            _characteristicConsoleService = characteristicConsoleService;
        }

        public void StartConsoleLoop()
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
                            _manufacturerConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 2:
                        {
                            _supplierConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 3:
                        {
                            _characteristicConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 4:
                        {
                            _categoryConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 5:
                        {
                            _productListConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 6:
                        {
                            _orderConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 7:
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

        private static void PrintMenu()
        {
            Console.WriteLine("1. Manufacturers");
            Console.WriteLine("2. Suppliers");
            Console.WriteLine("3. Characteristics");
            Console.WriteLine("4. Categories");
            Console.WriteLine("5. Products");
            Console.WriteLine("6. Orders");
            Console.WriteLine("7. Exit");
        }
    }
}