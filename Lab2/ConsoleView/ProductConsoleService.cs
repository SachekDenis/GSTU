using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using BusinessLogic.Models;
using ConsoleApp.ViewModels;

namespace ConsoleApp.ConsoleView
{
    public class ProductConsoleService
    {
        private readonly ProductManager _productManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly SupplyManager _supplyManager;
        private readonly SupplierManager _supplierManager;
        private readonly CategoryManager _categoryManager;
        private readonly ConsolePrinter _printer;
        private int _productId;

        public ProductConsoleService(ProductManager productManager,
            ManufacturerManager manufacturerManager,
            CharacteristicManager characteristicManager,
            SupplierManager supplierManager,
            SupplyManager supplyManager,
            CategoryManager categoryManager)
        {
            _productManager = productManager;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
            _supplyManager = supplyManager;
            _supplierManager = supplierManager;
            _categoryManager = categoryManager;
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
                            // Add();
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
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Назад");
        }

    }
}