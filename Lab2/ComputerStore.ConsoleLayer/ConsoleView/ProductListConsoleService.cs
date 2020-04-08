﻿using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Exception;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView
{
    internal class ProductListConsoleService:CrudConsoleService<Product>
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly ProductConsoleService _productConsoleService;
        private readonly ProductManager _productManager;
        private readonly SupplierManager _supplierManager;
        private readonly SupplyManager _supplyManager;

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
        }

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
                        {
                            Console.WriteLine("Enter Id of product");
                            var productId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

                            if (_productManager.GetById(productId) == null)
                            {
                                throw new InvalidOperationException();
                            }

                            _productConsoleService.ProductId = productId;

                            _productConsoleService.StartConsoleLoop();
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

        protected override void PrintAll()
        {
            var categories = _categoryManager.GetAll();
            var items = _productManager.GetAll().Select(item => new ProductListViewModel
            {
                Id = item.Id,
                Category = categories.First(category => category.Id == item.CategoryId).Name,
                Name = item.Name
            });

            items.WriteCollectionAsTable();
        }

        protected override void Add()
        {
            _supplierManager.GetAll().WriteCollectionAsTable();

            Console.WriteLine("Enter Id of manufacturer");

            var supplyDto = new Supply
            {
                Date = DateTime.Now,
                SupplierId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException())
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

        protected override void Delete()
        {
            Console.WriteLine("Enter Id of product to delete");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _productManager.Delete(id);
        }

        protected override Product CreateModel()
        {
            Console.WriteLine("Enter name of product");
            var name = Console.ReadLine();

            Console.WriteLine("Enter price of product");
            var price = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Console.WriteLine("Enter amount of product");
            var amount = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _manufacturerManager.GetAll().WriteCollectionAsTable();

            Console.WriteLine("Enter Id of manufacturer");
            var manufacturerId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            _categoryManager.GetAll().WriteCollectionAsTable();

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

            var productCharacteristics = _characteristicManager.GetAll()
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


        protected override void Update()
        {
            Console.WriteLine("Enter Id of product for update");
            var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var productDto = CreateModel();
            productDto.Id = id;

            var fieldList = productDto.Fields.ToList();

            fieldList.ForEach(field => { field.ProductId = productDto.Id; });

            productDto.Fields = fieldList;

            _productManager.Update(productDto);
        }


        protected override void PrintMenu()
        {
            Console.WriteLine("1. Print product details");
            Console.WriteLine("2. Add product");
            Console.WriteLine("3. Update product");
            Console.WriteLine("4. Delete product");
            Console.WriteLine("5. Back");
        }
    }
}