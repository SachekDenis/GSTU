using System;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class ProductConsoleService : BaseConsoleService
    {
        private readonly BuyerManager _buyerManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;

        public ProductConsoleService(ProductManager productManager,
            ManufacturerManager manufacturerManager,
            CharacteristicManager characteristicManager,
            OrderManager orderManager,
            BuyerManager buyerManager)
        {
            _productManager = productManager;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
            _orderManager = orderManager;
            _buyerManager = buyerManager;
        }

        public int ProductId { get; set; }

        public override void StartConsoleLoop()
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
                            Buy();
                            break;
                        case 2:
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

        private void Buy()
        {
            Console.WriteLine("Enter name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter surname");
            var surname = Console.ReadLine();
            Console.WriteLine("Enter e-mail");
            var email = Console.ReadLine();
            Console.WriteLine("Enter phone (Format: xxxxxxxxxxxx)");
            var phone = Console.ReadLine();
            Console.WriteLine("Enter address");
            var address = Console.ReadLine();
            Console.WriteLine("Enter zipcode");
            var zipCode = Console.ReadLine();


            var buyerDto = new Buyer
            {
                Address = address,
                FirstName = firstName,
                SecondName = surname,
                Email = email,
                PhoneNumber = phone,
                ZipCode = zipCode
            };

            _buyerManager.Add(buyerDto);

            Console.WriteLine("Enter amount");
            var amount = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var orderDto = new Order
            {
                ProductId = ProductId,
                BuyerId = buyerDto.Id,
                Amount = amount
            };

            try
            {
                var productDto = _productManager.GetById(ProductId);
                productDto.AmountInStorage -= amount;

                _productManager.Update(productDto);
                _orderManager.Add(orderDto);
            }
            catch (ValidationException)
            {
                _buyerManager.Delete(buyerDto.Id);
                throw;
            }
        }

        protected override void PrintAll()
        {
            var productDto = _productManager.GetById(ProductId);
            var productViewModel = new ProductViewModel
            {
                Amount = productDto.AmountInStorage,
                Fields = productDto.Fields,
                Manufacturer = _manufacturerManager.GetById(productDto.ManufacturerId).Name,
                Name = productDto.Name,
                Price = productDto.Price
            };

            Console.Write("Product name: ");
            Console.WriteLine(productViewModel.Name);
            Console.Write("Price: ");
            Console.WriteLine(productViewModel.Price);
            Console.Write("Amount: ");
            Console.WriteLine(productViewModel.Amount);
            Console.Write("Manufacturer: ");
            Console.WriteLine(productViewModel.Manufacturer);

            productViewModel.Fields.ToList().ForEach(field =>
            {
                Console.Write(_characteristicManager.GetById(field.CharacteristicId).Name);
                Console.Write(":");
                Console.Write(field.Value);
                Console.WriteLine();
            });
        }

        protected override void PrintMenu()
        {
            Console.WriteLine("1. Buy");
            Console.WriteLine("2. Back");
        }
    }
}