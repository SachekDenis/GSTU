using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class OrderConsoleService:IConsoleService
    {
        private readonly BuyerManager _buyerManager;
        private readonly OrderManager _orderManager;
        private readonly ProductConsoleService _productConsoleService;
        private readonly ProductManager _productManager;

        public OrderConsoleService(OrderManager orderManager,
            ProductManager productManager,
            BuyerManager buyerManager,
            ProductConsoleService productConsoleService)
        {
            _orderManager = orderManager;
            _productManager = productManager;
            _buyerManager = buyerManager;
            _productConsoleService = productConsoleService;
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

            new List<Buyer> {_buyerManager.GetById(buyerId)}.WriteCollectionAsTable();

            Console.ReadKey();
        }

        private void PrintProductInformation()
        {
            var orderId = ReadId();

            var productId = _orderManager.GetById(orderId).ProductId;

            _productConsoleService.ProductId = productId;

            _productConsoleService.StartConsoleLoop();
        }

        private void ChangeStatus()
        {
            var orderId = ReadId();

            var order = _orderManager.GetById(orderId);

            Console.WriteLine("Enter new status of order");
            Console.WriteLine("1. Created");
            Console.WriteLine("2. Executing");
            Console.WriteLine("3. Completed");

            var newStatus = (OrderStatus) (int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()) - 1);

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
            var items = _orderManager.GetAll()
                .Select(item => new OrderViewModel
                {
                    ProductName = _productManager.GetById(item.ProductId).Name,
                    BuyerAddress = _buyerManager.GetById(item.BuyerId).Address,
                    Count = item.Amount,
                    OrderStatus = item.OrderStatus,
                    Id = item.Id
                }).ToList();

            items.WriteCollectionAsTable();
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