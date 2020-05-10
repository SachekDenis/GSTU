using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<ProductsController> _logger;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly ProductManager _productManager;

        public ProductsController(ProductManager productManager,
                                  CategoryManager categoryManager,
                                  ManufacturerManager manufacturerManager,
                                  CharacteristicManager characteristicManager,
                                  ILogger<ProductsController> logger)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
            _logger = logger;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var productViewModels = (await _productManager.GetAll())
                                    .Select(product => CreateProductViewModel(product, categories, manufacturers))
                                    .OrderBy(product => product.CategoryName);

            return View(productViewModels);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var characteristics = await _characteristicManager.GetAll();
            var product = await _productManager.GetById(id);

            var productViewModel = CreateProductViewModel(product, categories, manufacturers);
            productViewModel.Fields = CreateFieldViewModels(product, characteristics);

            return View(productViewModel);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var characteristics = await _characteristicManager.GetAll();
            var productViewModel = new ProductViewModel
                                   {
                                       Fields = characteristics.Select(characteristic => new FieldViewModel
                                                                                         {
                                                                                             CharacteristicName =
                                                                                                 characteristic.Name,
                                                                                             CharacteristicId =
                                                                                                 characteristic.Id
                                                                                         }),
                                       Categories = new SelectList(await _categoryManager.GetAll(), "Id", "Name"),
                                       Manufacturers = new SelectList(await _manufacturerManager.GetAll(), "Id", "Name")
                                   };

            return View(productViewModel);
        }

        public async Task<ActionResult> SelectFieldsForCategory(int categoryId)
        {
            var characteristics = (await _characteristicManager.GetAll()).Where(characteristic => characteristic.CategoryId == categoryId);

            var fields = characteristics.Select(characteristic => new FieldViewModel
                                                                  {
                                                                      CharacteristicName = characteristic.Name,
                                                                      CharacteristicId = characteristic.Id
                                                                  });

            return PartialView(fields);
        }


        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel, IFormCollection formCollection)
        {
            productViewModel.Categories = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
            productViewModel.Manufacturers = new SelectList(await _manufacturerManager.GetAll(), "Id", "Name");

            try
            {
                var product = CreateProduct(productViewModel, formCollection);

                await _productManager.Add(product);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating product. Exception: {exception.Message}");
                return View(productViewModel);
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var characteristics = await _characteristicManager.GetAll();
            var product = await _productManager.GetById(id);
            var productViewModel = CreateProductViewModel(product, categories, manufacturers);
            productViewModel.Fields = CreateFieldViewModels(product, characteristics);

            productViewModel.Categories = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
            productViewModel.Manufacturers = new SelectList(await _manufacturerManager.GetAll(), "Id", "Name");

            var emptyFields = characteristics
                              .Where(characteristic =>
                                         characteristic.CategoryId == product.CategoryId &&
                                         !productViewModel.Fields.Any(field => field.CharacteristicId == characteristic.Id))
                              .Select(characteristic => new FieldViewModel
                                                        {
                                                            CharacteristicId = characteristic.Id,
                                                            CharacteristicName = characteristic.Name
                                                        })
                              .ToList();

            productViewModel.Fields = productViewModel.Fields.Union(emptyFields).ToList();

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel, IFormCollection formCollection)
        {
            var characteristics = await _characteristicManager.GetAll();
            productViewModel.Categories = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
            productViewModel.Manufacturers = new SelectList(await _manufacturerManager.GetAll(), "Id", "Name");
            var product = CreateProduct(productViewModel, formCollection);
            productViewModel.Fields = CreateFieldViewModels(product, characteristics);

            try
            {
                product.Id = productViewModel.Id;

                await _productManager.Update(product);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing product. Exception: {exception.Message}");
                return View(productViewModel);
            }
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var product = await _productManager.GetById(id);
            var productViewModel = CreateProductViewModel(product, categories, manufacturers);
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ProductViewModel product)
        {
            try
            {
                await _productManager.Delete(product.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting product. Exception: {exception.Message}");
                return View(product);
            }
        }

        private ProductViewModel CreateProductViewModel(Product product, IEnumerable<Category> categories, IEnumerable<Manufacturer> manufacturers)
        {
            return new ProductViewModel
                   {
                       Id = product.Id,
                       AmountInStorage = product.AmountInStorage,
                       CategoryName = categories.First(category => category.Id == product.CategoryId).Name,
                       ManufacturerName = manufacturers.First(manufacturer => manufacturer.Id == product.ManufacturerId).Name,
                       Name = product.Name,
                       Price = product.Price,
                       CategoryId = product.CategoryId,
                       ManufacturerId = product.ManufacturerId
                   };
        }

        private IEnumerable<FieldViewModel> CreateFieldViewModels(Product product, IEnumerable<Characteristic> characteristics)
        {
            return product.Fields.Select(field => new FieldViewModel
                                                  {
                                                      CharacteristicId = field.CharacteristicId,
                                                      CharacteristicName = characteristics
                                                                           .First(characteristic => characteristic.Id == field.CharacteristicId)
                                                                           .Name,
                                                      Id = field.Id,
                                                      ProductId = field.ProductId,
                                                      Value = field.Value
                                                  });
        }

        private Product CreateProduct(ProductViewModel productViewModel, IFormCollection formCollection)
        {
            var fields = formCollection.Where(formPair => formPair.Key.Contains("characteristic"))
                                       .Select(formPair => new Field
                                                           {
                                                               CharacteristicId = int.Parse(formPair.Key.Substring(formPair.Key.IndexOf('-') + 1)),
                                                               Value = formPair.Value
                                                           });

            return new Product
                   {
                       AmountInStorage = productViewModel.AmountInStorage,
                       CategoryId = productViewModel.CategoryId,
                       Fields = fields,
                       ManufacturerId = productViewModel.ManufacturerId,
                       Name = productViewModel.Name,
                       Price = productViewModel.Price
                   };
        }
    }
}