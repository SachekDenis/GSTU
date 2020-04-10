using System;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices
{
    public class ProductBaseConsoleService : IConsoleService
    {
        private readonly BuyerManager _buyerManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;

        public ProductBaseConsoleService(ProductManager productManager,
            OrderManager orderManager,
            BuyerManager buyerManager,
            ManufacturerManager manufacturerManager,
            CharacteristicManager characteristicManager)
        {
            _productManager = productManager;
            _orderManager = orderManager;
            _buyerManager = buyerManager;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
        }

        public int ProductId { get; set; }

        public async Task StartConsoleLoop()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    await PrintProductDetails();
                    PrintMenu();

                    var menuTab = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                    switch (menuTab)
                    {
                        case 1:
                            await Buy();
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

        public void PrintMenu()
        {
            Console.WriteLine("1. Buy");
            Console.WriteLine("2. Back");
        }

        private async Task Buy()
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

            await _buyerManager.Add(buyerDto);

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
                var productDto = await _productManager.GetById(ProductId);
                productDto.AmountInStorage -= amount;

                await _productManager.Update(productDto);
                await _orderManager.Add(orderDto);
            }
            catch (ValidationException)
            {
                await _buyerManager.Delete(buyerDto.Id);
                throw;
            }
        }

        private async Task PrintProductDetails()
        {
            var productDto = await _productManager.GetById(ProductId);
            var productViewModel = new ProductViewModel
            {
                Amount = productDto.AmountInStorage,
                Fields = productDto.Fields,
                Manufacturer = (await _manufacturerManager.GetById(productDto.ManufacturerId)).Name,
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

            var characteristics = await _characteristicManager.GetAll();

            productViewModel.Fields.ToList().ForEach(field =>
            {
                Console.Write(characteristics.First(characteristic => characteristic.Id == field.CharacteristicId)
                    .Name);
                Console.Write(":");
                Console.Write(field.Value);
                Console.WriteLine();
            });
        }
    }
}