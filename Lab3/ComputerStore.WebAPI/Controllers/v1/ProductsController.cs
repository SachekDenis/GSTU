using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebAPI.Models;
using ComputerStore.WebUI.AppConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebAPI.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    { 
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<ProductsController> _logger;
        private readonly ManufacturerManager _manufacturerManager;
        private readonly ProductManager _productManager;

        public ProductsController(
            ProductManager productManager, 
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = (await _productManager.GetAll()).ToList();

            return products;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var product = await _productManager.GetById(id);

                return product;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during getting product. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            try
            {
                await _productManager.Add(product);

                return Ok(product);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating product. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPut]
        public async Task<ActionResult<Product>> Put([FromBody] Product product)
        {
            try
            {
                await _productManager.Update(product);

                return Ok(product);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing product. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productManager.Delete(id);

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