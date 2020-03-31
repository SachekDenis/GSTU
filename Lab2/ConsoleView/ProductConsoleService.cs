using System;
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

        public async Task StartConsoleLoop(int productId)
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
                            await Add();
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
                Count = product.Count,
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

        private async Task Add()
        {
            _printer.WriteCollectionAsTable(_supplierManager.GetAll());
            var supplyDto = new SupplyDto();
            Console.WriteLine("Введите Id поставщика");
            supplyDto.Date = DateTime.Now;
            supplyDto.SupplierId = int.Parse(Console.ReadLine());
            await _supplyManager.Add(supplyDto);

            var product = CreateModel();
            product.SupplyId = supplyDto.Id;

            await _productManager.Add(product);

        }

        private ProductDto CreateModel()
        {
            Console.WriteLine("Введите наименование товара");
            var name = Console.ReadLine();
            Console.WriteLine("Введите цену товара");
            var price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество товара");
            var count = int.Parse(Console.ReadLine());
            _printer.WriteCollectionAsTable(_manufacturerManager.GetAll());
            Console.WriteLine("Введите Id производителя");
            var manufacturerId = int.Parse(Console.ReadLine());
            _printer.WriteCollectionAsTable(_categoryManager.GetAll());
            Console.WriteLine("Введите Id категории");
            var categoryId = int.Parse(Console.ReadLine());


            var productDto = new ProductDto()
            {
                Name = name,
                Price = price,
                ManufacturerId = manufacturerId,
                CategoryId = categoryId,
                Count = count,
            };

            return productDto;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Назад");
        }

    }
}