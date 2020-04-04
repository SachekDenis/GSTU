using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
                            {
                                ChangeStatus();
                            }
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

        private void PrintBuyerInformation()
        {
            var orderId = ReadId();

            var buyerId = _orderManager.GetById(orderId).BuyerId;

            _printer.WriteCollectionAsTable(new List<BuyerDto> { _buyerManager.GetById(buyerId) });

            Console.ReadKey();
        }

        private void PrintProductInformation()
        {
            var orderId = ReadId();

            var productId = _orderManager.GetById(orderId).ProductId;

            _productConsoleService.StartConsoleLoop(productId);
        }

        private void ChangeStatus()
        {
            var orderId = ReadId();

            var order = _orderManager.GetById(orderId);

            Console.WriteLine("Enter new status of order");
            Console.WriteLine("1. Created");
            Console.WriteLine("2. Executing");
            Console.WriteLine("3. Completed");

            var newStatus = (OrderStatusDto)(int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()) - 1);

            order.OrderStatus = newStatus;

            _orderManager.Update(order);


        }

        private int ReadId()
        {
            Console.WriteLine("Enter Id of order");

            return int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        }


        private void PrintAll()
        {
            var items = _orderManager.GetAll().ToList()
                .Select(item => new OrderViewModel
                {
                    ProductName = _productManager.GetById(item.ProductId).Name,
                    BuyerAddress = _buyerManager.GetById(item.BuyerId).Address,
                    Count = item.Amount,
                    OrderStatus = item.OrderStatus,
                    Id = item.Id
                }).ToList();

            _printer.WriteCollectionAsTable(items);
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Print details of buyer");
            Console.WriteLine("2. Print details of product");
            Console.WriteLine("3. Change order status");
            Console.WriteLine("4. Back");
        }
    }
}