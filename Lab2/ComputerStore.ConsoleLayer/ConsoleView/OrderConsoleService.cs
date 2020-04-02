using System;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class OrderConsoleService
    {
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;
        private readonly ConsolePrinter _printer;
        private readonly BuyerManager _buyerManager;

        public OrderConsoleService(OrderManager orderManager, ProductManager productManager, BuyerManager buyerManager)
        {
            _orderManager = orderManager;
            _productManager = productManager;
            _buyerManager = buyerManager;
            _printer = new ConsolePrinter();
        }

        public void StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 2:
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private void PrintAll()
        {
            var items = _orderManager.GetAll().ToList()
                .Select(item => new OrderViewModel
                {
                     ProductName = _productManager.GetById(item.ProductId).Name,
                     BuyerAddress = _buyerManager.GetById(item.BuyerId).Address,
                     Count = item.Count,
                     Id = item.Id
                }).ToList();

            _printer.WriteCollectionAsTable(items);
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Вывести детали покупателя");
        }
    }
}