using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    public class OrderBaseConsoleService : IConsoleService
    {
        private readonly BuyerManager _buyerManager;
        private readonly OrderManager _orderManager;
        private readonly IPrintConsoleService _printOrderService;
        private readonly ProductBaseConsoleService _productConsoleService;

        public OrderBaseConsoleService(OrderManager orderManager,
            BuyerManager buyerManager,
            ProductBaseConsoleService productConsoleService,
            OrderPrintConsoleService printOrderService)
        {
            _orderManager = orderManager;
            _buyerManager = buyerManager;
            _productConsoleService = productConsoleService;
            _printOrderService = printOrderService;
        }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await _printOrderService.PrintAll();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                        {
                            await PrintBuyerInformation();
                        }
                            break;
                        case 2:
                        {
                            await PrintProductInformation();
                        }
                            break;
                        case 3:
                        {
                            await ChangeStatus();
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

        public void PrintMenu()
        {
            Console.WriteLine("1. Print details of buyer");
            Console.WriteLine("2. Print details of product");
            Console.WriteLine("3. Change order status");
            Console.WriteLine("4. Back");
        }

        private async Task PrintBuyerInformation()
        {
            var orderId = ReadId();

            var buyerId = (await _orderManager.GetById(orderId)).BuyerId;

            new List<Buyer> {await _buyerManager.GetById(buyerId)}.WriteCollectionAsTable();

            Console.ReadKey();
        }

        private async Task PrintProductInformation()
        {
            var orderId = ReadId();

            var productId = (await _orderManager.GetById(orderId)).ProductId;

            _productConsoleService.ProductId = productId;

            await _productConsoleService.StartConsoleLoop();
        }

        private async Task ChangeStatus()
        {
            var orderId = ReadId();

            var order = await _orderManager.GetById(orderId);

            Console.WriteLine("Enter new status of order");
            Console.WriteLine("1. Created");
            Console.WriteLine("2. Executing");
            Console.WriteLine("3. Completed");

            var newStatus = (OrderStatus) (int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()) - 1);

            order.OrderStatus = newStatus;

            await _orderManager.Update(order);
        }

        private int ReadId()
        {
            Console.WriteLine("Enter Id of order");

            return int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        }
    }
}