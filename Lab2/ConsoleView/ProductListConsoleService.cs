using BusinessLogic.Managers;
using ConsoleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace ConsoleApp.ConsoleView
{
    class ProductListConsoleService
    {
        private readonly ProductManager _productManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly SupplyManager _supplyManager;
        private readonly SupplierManager _supplierManager;
        private readonly CategoryManager _categoryManager;
        private readonly ConsolePrinter _printer;
        private readonly ProductConsoleService _productConsoleService;

        public ProductListConsoleService(ProductManager productManager,
            CategoryManager categoryManager,
            ProductConsoleService productConsoleService,
            ManufacturerManager manufacturerManager,
            CharacteristicManager characteristicManager,
            SupplyManager supplyManager,
            SupplierManager supplierManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _productConsoleService = productConsoleService;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
            _supplyManager = supplyManager;
            _supplierManager = supplierManager;
            _printer = new ConsolePrinter();
        }

        public async Task StartConsoleLoop()
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
                                var productId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                                await _productConsoleService.StartConsoleLoop(productId);
                            }
                            break;
                        case 2:
                            {
                                await Add();
                            }
                            break;
                        case 3:
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
            var categories = _categoryManager.GetAll();
            var items = _productManager.GetAll().Select(item => new ProductListViewModel()
            {
                Id = item.Id,
                Category = categories.First(category => category.Id == item.CategoryId).Name,
                Name = item.Name
            });
            _printer.WriteCollectionAsTable(items);
        }

        private async Task Add()
        {
            _printer.WriteCollectionAsTable(_supplierManager.GetAll());
            Console.WriteLine("Введите Id поставщика");

            var supplyDto = new SupplyDto
            {
                Date = DateTime.Now,
                SupplierId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException())
            };

            await _supplyManager.Add(supplyDto);

            var product = CreateModel();
            product.SupplyId = supplyDto.Id;

            var productCharacteristics = _characteristicManager.GetAll()
                .Where(characteristicDto => characteristicDto.CategoryId == product.CategoryId).ToList();

            product.Fields = new List<FieldDto>();

            productCharacteristics.ForEach(characteristic =>
            {
                Console.WriteLine($"Введите значение характеристики {characteristic.Name}");
                var value = Console.ReadLine();
                product.Fields.Add(new FieldDto()
                {
                    CharacteristicId = characteristic.Id,
                    Value = value
                });
            });

            try
            {
                await _productManager.Add(product);
            }
            catch (ValidationException)
            {
                await _supplyManager.Delete(supplyDto.Id);
                throw;
            }


        }

        private ProductDto CreateModel()
        {
            Console.WriteLine("Введите наименование товара");
            var name = Console.ReadLine();
            Console.WriteLine("Введите цену товара");
            var price = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.WriteLine("Введите количество товара");
            var count = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _printer.WriteCollectionAsTable(_manufacturerManager.GetAll());
            Console.WriteLine("Введите Id производителя");
            var manufacturerId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _printer.WriteCollectionAsTable(_categoryManager.GetAll());
            Console.WriteLine("Введите Id категории");
            var categoryId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());


            var productDto = new ProductDto()
            {
                Name = name,
                Price = price,
                ManufacturerId = manufacturerId,
                CategoryId = categoryId,
                CountInStorage = count,
            };

            return productDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Вывести детали товара");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Назад");
        }
    }
}
