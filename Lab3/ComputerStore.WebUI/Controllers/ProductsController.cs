using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.AppConfiguration;
using ComputerStore.WebUI.Mappers;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
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
                                    .Select(product => product.CreateProductViewModel(categories, manufacturers))
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

            var productViewModel = product.CreateProductViewModel(categories, manufacturers);
            productViewModel.Fields = product.CreateFieldViewModels(characteristics);

            return View(productViewModel);
        }

        // GET: Products/Create
        [Authorize(Roles = RolesNames.Admin)]
        public async Task<ActionResult> Create()
        {
            var characteristics = await _characteristicManager.GetAll();
            var productViewModel = new ProductViewModel
                                   {
                                       Fields = characteristics.Select(characteristic => new FieldViewModel
                                                                                         {
                                                                                             CharacteristicName = characteristic.Name,
                                                                                             CharacteristicId = characteristic.Id
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
        [Authorize(Roles = RolesNames.Admin)]
        public async Task<ActionResult> Create(ProductViewModel productViewModel, IFormCollection formCollection)
        {
            productViewModel.Categories = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
            productViewModel.Manufacturers = new SelectList(await _manufacturerManager.GetAll(), "Id", "Name");

            try
            {
                var product = productViewModel.CreateProductWithFields(formCollection);

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
        [Authorize(Roles = RolesNames.Admin)]
        public async Task<ActionResult> Edit(int id)
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var characteristics = await _characteristicManager.GetAll();
            var product = await _productManager.GetById(id);
            var productViewModel = product.CreateProductViewModel(categories, manufacturers);
            productViewModel.Fields = product.CreateFieldViewModels(characteristics);

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
        [Authorize(Roles = RolesNames.Admin)]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel, IFormCollection formCollection)
        {
            var characteristics = await _characteristicManager.GetAll();
            productViewModel.Categories = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
            productViewModel.Manufacturers = new SelectList(await _manufacturerManager.GetAll(), "Id", "Name");
            var product = productViewModel.CreateProductWithFields(formCollection);
            productViewModel.Fields = product.CreateFieldViewModels(characteristics);

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
        [Authorize(Roles = RolesNames.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var product = await _productManager.GetById(id);
            var productViewModel = product.CreateProductViewModel(categories, manufacturers);
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesNames.Admin)]
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
    }
}