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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/characteristics")]
    [ApiController]
    [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
    public class CharacteristicsApiController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<CharacteristicsApiController> _logger;

        public CharacteristicsApiController(CharacteristicManager characteristicManager,
                                            CategoryManager categoryManager, 
                                            ILogger<CharacteristicsApiController> logger)
        {
            _characteristicManager = characteristicManager;
            _categoryManager = categoryManager;
            _logger = logger;
        }

        [HttpGet("characteristics")]
        public async Task<IEnumerable<CharacteristicViewModel>> Characteristics()
        {
            var categories = await _categoryManager.GetAll();
            var characteristics = (await _characteristicManager.GetAll())
                                  .Select(characteristic => characteristic.CreateCharacteristicViewModel(categories))
                                  .OrderBy(characteristic => characteristic.CategoryName);

            return characteristics;
        }

        [HttpGet("details")]
        public async Task<CharacteristicViewModel> Details([FromQuery] int id)
        {
            var categories = await _categoryManager.GetAll();
            var characteristic = (await _characteristicManager.GetById(id)).CreateCharacteristicViewModel(categories);

            return characteristic;
        }


        [HttpPost("create")]
        public async Task<StatusCodeResult> Create([FromBody] CharacteristicViewModel characteristicViewModel)
        {
            try
            {
                await _characteristicManager.Add(new Characteristic
                                                 {
                                                     CategoryId = characteristicViewModel.CategoryId,
                                                     Name = characteristicViewModel.Name
                                                 });

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating characteristic. Exception: {exception.Message}");
                characteristicViewModel.CategoriesSelectList = new SelectList(await _categoryManager.GetAll(), "Id", "Name");
                return BadRequest();
            }
        }

        // POST: Characteristics/Edit/5
        [HttpPost("edit")]
        public async Task<StatusCodeResult> Edit([FromBody] CharacteristicViewModel characteristicViewModel)
        {
            try
            {
                await _characteristicManager.Update(new Characteristic
                                                    {
                                                        Id = characteristicViewModel.Id,
                                                        CategoryId = characteristicViewModel.CategoryId,
                                                        Name = characteristicViewModel.Name
                                                    });

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing characteristic. Exception: {exception.Message}");
                return BadRequest();
            }
        }


        [HttpPost("delete")]
        public async Task<ActionResult> Delete([FromBody] CharacteristicViewModel characteristicViewModel)
        {
            try
            {
                await _characteristicManager.Delete(characteristicViewModel.Id);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting characteristic. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}
