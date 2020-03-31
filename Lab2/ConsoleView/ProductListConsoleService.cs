using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
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
                                var productId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                                _productConsoleService.StartConsoleLoop(productId);
                            }
                            break;
                        case 2:
                            {
                                Add();
                            }
                            break;
                        case 3:
                            {
                                Update();
                            }
                            break;
                        case 4:
                            {
                                Delete();
                            }
                            break;
                        case 5:
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

        private void Add()
        {
            _printer.WriteCollectionAsTable(_supplierManager.GetAll());

            Console.WriteLine("Введите Id поставщика");

            var supplyDto = new SupplyDto
            {
                Date = DateTime.Now,
                SupplierId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException()),
            };

            var product = CreateModel();

            try
            {
                _productManager.Add(product);
                supplyDto.ProductId = product.Id;
                _supplyManager.Add(supplyDto);
            }
            catch (ValidationException)
            {
                _supplyManager.Delete(supplyDto.Id);
                throw;
            }


        }

        private void Delete()
        {
            Console.WriteLine("Введите Id для удаления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _productManager.Delete(id);
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

            var productCharacteristics = _characteristicManager.GetAll()
                .Where(characteristicDto => characteristicDto.CategoryId == productDto.CategoryId).ToList();

            productDto.Fields = new List<FieldDto>();

            productCharacteristics.ForEach(characteristic =>
            {
                Console.WriteLine($"Введите значение характеристики {characteristic.Name}");
                var value = Console.ReadLine();
                productDto.Fields.Add(new FieldDto()
                {
                    CharacteristicId = characteristic.Id,
                    Value = value
                });
            });


            return productDto;
        }


        private void Update()
        {
            Console.WriteLine("Введите Id для обновления");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var productDto = CreateModel();
            productDto.Id = id;
            productDto.Fields.ForEach(field => { field.ProductId = productDto.Id; });

            _productManager.Update(productDto);
        }


        private static void PrintMenu()
        {
            Console.WriteLine("1. Вывести детали товара");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Обновить товар");
            Console.WriteLine("4. Удалить товар");
            Console.WriteLine("5. Назад");
        }
    }
}
