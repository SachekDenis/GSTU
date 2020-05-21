using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Controllers;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesApiController(CategoryManager categoryManager, 
                                       ILogger<CategoriesController> logger)
        {
            _categoryManager = categoryManager;
            _logger = logger;
        }

        [HttpGet("categories")]
        public async Task<IEnumerable<CategoryViewModel>> Categories()
        {
            var categoryViewModels = (await _categoryManager.GetAll()).Select(CreateCategoryViewModel);
            return categoryViewModels;
        }

        [HttpGet("details")]
        public async Task<CategoryViewModel> Details([FromQuery] int id)
        {
            var category = await _categoryManager.GetById(id);

            var categoryViewModel = CreateCategoryViewModel(category);

            return categoryViewModel;
        }

        [HttpPost("create")]
        public async Task<StatusCodeResult> Create([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Add(new Category
                                           {
                                               Id = categoryViewModel.Id,
                                               Name = categoryViewModel.Name
                                           });

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating category. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        // POST: Categories/Edit/5
        [HttpPost("edit")]
        public async Task<StatusCodeResult> Edit([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Update(new Category
                                              {
                                                  Id = categoryViewModel.Id,
                                                  Name = categoryViewModel.Name
                                              });
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating category. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPost("delete")]
        public async Task<StatusCodeResult> Delete([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                await _categoryManager.Delete(categoryViewModel.Id);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting category. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        private CategoryViewModel CreateCategoryViewModel(Category category)
        {
            return new CategoryViewModel
                   {
                       Id = category.Id,
                       Name = category.Name
                   };
        }
    }
}