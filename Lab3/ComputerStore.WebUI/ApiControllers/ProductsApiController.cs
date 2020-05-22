using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.AppConfiguration;
using ComputerStore.WebUI.Controllers;
using ComputerStore.WebUI.Mappers;
using ComputerStore.WebUI.Models;
using ComputerStore.WebUI.Models.JwtToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    { 
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<ProductsApiController> _logger;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly ProductManager _productManager;

        public ProductsApiController(ProductManager productManager,
                                     CategoryManager categoryManager,
                                     ManufacturerManager manufacturerManager,
                                     CharacteristicManager characteristicManager,
                                     ILogger<ProductsApiController> logger)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _manufacturerManager = manufacturerManager;
            _characteristicManager = characteristicManager;
            _logger = logger;
        }

        [HttpGet("products")]
        public async Task<IEnumerable<ProductViewModel>> Products()
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var productViewModels = (await _productManager.GetAll())
                                    .Select(product => product.CreateProductViewModel(categories, manufacturers))
                                    .OrderBy(product => product.CategoryName);

            return productViewModels;
        }

        [HttpGet("details")]
        public async Task<ProductViewModel> Details([FromQuery] int id)
        {
            var categories = await _categoryManager.GetAll();
            var manufacturers = await _manufacturerManager.GetAll();
            var characteristics = await _characteristicManager.GetAll();
            var product = await _productManager.GetById(id);

            var productViewModel = product.CreateProductViewModel(categories, manufacturers);
            productViewModel.Fields = product.CreateFieldViewModels(characteristics);

            return productViewModel;
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPost("create")]
        public async Task<StatusCodeResult> Create([FromBody] ProductViewModel productViewModel)
        {
            try
            {
                var product = productViewModel.CreateProduct();

                await _productManager.Add(product);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating product. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPost("edit")]
        public async Task<StatusCodeResult> Edit([FromBody] ProductViewModel productViewModel)
        {
            var characteristics = await _characteristicManager.GetAll();
            var product = productViewModel.CreateProduct();
            productViewModel.Fields = product.CreateFieldViewModels(characteristics);

            try
            {
                product.Id = productViewModel.Id;

                await _productManager.Update(product);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing product. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPost("delete")]
        public async Task<StatusCodeResult> Delete([FromBody] ProductViewModel product)
        {
            try
            {
                await _productManager.Delete(product.Id);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting product. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}