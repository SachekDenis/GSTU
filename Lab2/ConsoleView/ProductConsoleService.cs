using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using ConsoleApp.ViewModels;

namespace ConsoleApp.ConsoleView
{
    public class ProductConsoleService
    {
        private readonly ProductManager _productManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly CharacteristicManager _characteristicManager;
        private int _productId;

        public ProductConsoleService(ProductManager productManager,
            ManufacturerManager manufacturerManager,
            CharacteristicManager characteristicManager)
        {
            _productManager = productManager;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
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
                            //await Add();
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
            var productViewModel = _productManager.GetAll().Where(product => product.Id ==_productId).Select(product => new ProductViewModel()
            {
                Count = product.Count,
                Fields = product.Fields,
                Manufacturer = _manufacturerManager.GetAll().First(item=>item.Id==product.ManufacturerId).Name,
                Name = product.Name,
                Price = product.Price,
            }).First();

                Console.WriteLine("Имя товара");
                Console.WriteLine(productViewModel.Name);
                Console.WriteLine("Цена товара");
                Console.WriteLine(productViewModel.Price);
                Console.WriteLine("Производитель");
                Console.WriteLine(productViewModel.Manufacturer);

                productViewModel.Fields.ForEach(field =>
                {
                    Console.Write(_characteristicManager.GetAll().First(characteristic => characteristic.Id==field.CharacteristicId).Name);
                    Console.Write(":");
                    Console.Write(field.Value);
                    Console.WriteLine();
                });
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1. Изменить товар");
            Console.WriteLine("2. Назад");
        }

    }
}