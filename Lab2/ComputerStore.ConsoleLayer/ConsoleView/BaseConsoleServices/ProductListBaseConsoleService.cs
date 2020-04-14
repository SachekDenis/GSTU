using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    internal class ProductListBaseConsoleService : IConsoleService
    {
        private readonly ICrudConsoleService<Product> _crudProductService;
        private readonly IPrintConsoleService _printProductService;
        private readonly ProductBaseConsoleService _productConsoleService;
        private readonly ProductManager _productManager;

        public ProductListBaseConsoleService(ProductBaseConsoleService productConsoleService,
            ICrudConsoleService<Product> crudProductService,
            ProductListPrintConsoleService printProductService,
            ProductManager productManager)
        {
            _productConsoleService = productConsoleService;
            _crudProductService = crudProductService;
            _printProductService = printProductService;
            _productManager = productManager;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printProductService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                        {
                            Console.WriteLine("Enter Id of product");
                            var productId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                            if (await _productManager.GetById(productId) == null)
                            {
                                throw new InvalidOperationException();
                            }

                            _productConsoleService.ProductId = productId;

                            await _productConsoleService.StartConsoleLoop();
                        }
                            break;
                        case 2:
                        {
                            await _crudProductService.Add();
                        }
                            break;
                        case 3:
                        {
                            await _crudProductService.Update();
                        }
                            break;
                        case 4:
                        {
                            await _crudProductService.Delete();
                        }
                            break;
                        case 5:
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
            Console.WriteLine("1. Print product details");
            Console.WriteLine("2. Add product");
            Console.WriteLine("3. Update product");
            Console.WriteLine("4. Delete product");
            Console.WriteLine("5. Back");
        }
    }
}