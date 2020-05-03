using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ConsoleView.CrudConsoleServices
{
    public class ProductListCrudConsoleService : ICrudConsoleService<Product>
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly ProductManager _productManager;

        public ProductListCrudConsoleService(CategoryManager categoryManager,
                                             CharacteristicManager characteristicManager,
                                             ManufacturerManager manufacturerManager,
                                             ProductManager productManager)
        {
            _categoryManager = categoryManager;
            _characteristicManager = characteristicManager;
            _manufacturerManager = manufacturerManager;
            _productManager = productManager;
        }

        public async Task Add()
        {
            Console.WriteLine("Enter Id of manufacturer");
            var product = await CreateModel();


            await _productManager.Add(product);
        }

        public async Task Delete()
        {
            Console.WriteLine("Enter Id of product to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            await _productManager.Delete(id);
        }

        public async Task<Product> CreateModel()
        {
            Console.WriteLine("Enter name of product");
            var name = Console.ReadLine();

            Console.WriteLine("Enter price of product");
            var price = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Console.WriteLine("Enter amount of product");
            var amount = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            (await _manufacturerManager.GetAll()).WriteCollectionAsTable();

            Console.WriteLine("Enter Id of manufacturer");
            var manufacturerId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            (await _categoryManager.GetAll()).WriteCollectionAsTable();

            Console.WriteLine("Enter Id of category");
            var categoryId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            var productDto = new Product
            {
                Name = name,
                Price = price,
                ManufacturerId = manufacturerId,
                CategoryId = categoryId,
                AmountInStorage = amount
            };

            var productCharacteristics = (await _characteristicManager.GetAll())
                .Where(characteristicDto => characteristicDto.CategoryId == productDto.CategoryId).ToList();

            var fieldList = new List<Field>();

            productCharacteristics.ForEach(characteristic =>
            {
                Console.WriteLine($"Enter value of characteristic {characteristic.Name}");
                var value = Console.ReadLine();
                fieldList.Add(new Field
                {
                    CharacteristicId = characteristic.Id,
                    Value = value
                });
            });

            productDto.Fields = fieldList;


            return productDto;
        }


        public async Task Update()
        {
            Console.WriteLine("Enter Id of product for update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var productDto = await CreateModel();
            productDto.Id = id;

            var fieldList = productDto.Fields.ToList();

            fieldList.ForEach(field => { field.ProductId = productDto.Id; });

            productDto.Fields = fieldList;

            await _productManager.Update(productDto);
        }
    }
}