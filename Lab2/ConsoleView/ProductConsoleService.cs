using System;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    public class ProductConsoleService
    {
        private readonly ProductManager _productManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly BuyerManager _buyerManager;
        private readonly OrderManager _orderManager;
        private readonly ConsolePrinter _printer;
        private int _productId;

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
            _printer = new ConsolePrinter();
        }

        public void StartConsoleLoop(int productId)
        {
            _productId = productId;
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

        private void Buy()
        {
            Console.WriteLine("Введите имя");
            var firstName = Console.ReadLine();
            Console.WriteLine("Введите фамилию");
            var surname = Console.ReadLine();
            Console.WriteLine("Введите e-mail");
            var email = Console.ReadLine();
            Console.WriteLine("Введите телефон");
            var phone = Console.ReadLine();
            Console.WriteLine("Введите адрес");
            var address = Console.ReadLine();
            Console.WriteLine("Введите почтовый индекс");
            var zipCode = Console.ReadLine();


            var buyerDto = new BuyerDto()
            {
                Address = address,
                FirstName = firstName,
                SecondName = surname,
                Email = email,
                PhoneNumber = phone,
                ZipCode = zipCode
            };

            _buyerManager.Add(buyerDto);

            Console.WriteLine("Введите количество");
            var count = int.Parse(Console.ReadLine());

            var orderDto = new OrderDto()
            {
                ProductId = _productId,
                BuyerId = buyerDto.Id,
                Count = count
            };

            try
            {
                var productDto = _productManager.GetAll().Where(productDto => productDto.Id == _productId).First();
                productDto.CountInStorage -= count;

                _productManager.Update(productDto);
                _orderManager.Add(orderDto);
            }
            catch(ValidationException)
            {
                _buyerManager.Delete(buyerDto.Id);
                throw;
            }


        }

        private void PrintAll()
        {
            var productViewModel = _productManager.GetAll().Where(product => product.Id == _productId).Select(product => new ProductViewModel()
            {
                Count = product.CountInStorage,
                Fields = product.Fields,
                Manufacturer = _manufacturerManager.GetAll().First(item => item.Id == product.ManufacturerId).Name,
                Name = product.Name,
                Price = product.Price,
            }).First();

            Console.Write("Имя товара: ");
            Console.WriteLine(productViewModel.Name);
            Console.Write("Цена товара: ");
            Console.WriteLine(productViewModel.Price);
            Console.Write("Количество товара: ");
            Console.WriteLine(productViewModel.Count);
            Console.Write("Производитель: ");
            Console.WriteLine(productViewModel.Manufacturer);

            productViewModel.Fields.ForEach(field =>
            {
                Console.Write(_characteristicManager.GetAll().First(characteristic => characteristic.Id == field.CharacteristicId).Name);
                Console.Write(":");
                Console.Write(field.Value);
                Console.WriteLine();
            });
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Купить");
            Console.WriteLine("2. Назад");
        }

    }
}