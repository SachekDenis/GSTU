using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class OrderConsoleService
    {
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;
        private readonly ConsolePrinter _printer;
        private readonly BuyerManager _buyerManager;
        private readonly ProductConsoleService _productConsoleService;

        public OrderConsoleService(OrderManager orderManager,
            ProductManager productManager,
            BuyerManager buyerManager,
            ProductConsoleService productConsoleService)
        {
            _orderManager = orderManager;
            _productManager = productManager;
            _buyerManager = buyerManager;
            _productConsoleService = productConsoleService;
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
                        case 1:
                            {
                                PrintBuyerInformation();
                            }
                            break;
                        case 2:
                            {
                                PrintProductInformation();
                            }
                            break;
                        case 3:
                            return;
                        default:
                            break;
                    }
                }
                catch (ValidationException e)
                {
                    Console.WriteLine($"Ошибка валидации. Сообщение {e.Message}");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private void PrintBuyerInformation()
        {
            Console.WriteLine("Введите Id заказа");

            var orderId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var buyerId = _orderManager.GetById(orderId).BuyerId;

            _printer.WriteCollectionAsTable(new List<BuyerDto> { _buyerManager.GetById(buyerId) });

            Console.ReadKey();
        }

        private void PrintProductInformation()
        {
            Console.WriteLine("Введите Id заказа");

            var orderId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var productId = _orderManager.GetById(orderId).ProductId;

            _productConsoleService.StartConsoleLoop(productId);
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
            Console.WriteLine("2. Вывести детали товара");
            Console.WriteLine("3. Назад");
        }
    }
}