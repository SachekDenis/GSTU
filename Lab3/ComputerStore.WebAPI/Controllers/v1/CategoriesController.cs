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
    [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            CategoryManager categoryManager, 
            ILogger<CategoriesController> logger)
        {
            _categoryManager = categoryManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var categories = (await _categoryManager.GetAll()).ToList();
            return categories;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            try
            {
                var category = await _categoryManager.GetById(id);

                return category;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during getting category. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            try
            {
                await _categoryManager.Add(category);
                return Ok(category);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating category. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<Category>> Put([FromBody] Category category)
        {
            try
            {
                await _categoryManager.Update(category);
                return Ok(category);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating category. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryManager.Delete(id);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting category. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}