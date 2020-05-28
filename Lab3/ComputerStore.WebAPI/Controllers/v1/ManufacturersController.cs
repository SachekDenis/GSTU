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
    public class ManufacturersController : ControllerBase
    {
        private readonly ILogger<ManufacturersController> _logger;
        private readonly ManufacturerManager _manufacturerManager;

        public ManufacturersController(
            ManufacturerManager manufacturerManager, 
            ILogger<ManufacturersController> logger)
        {
            _manufacturerManager = manufacturerManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> Get()
        {
            var manufacturerViewModels = (await _manufacturerManager.GetAll()).ToList();

            return manufacturerViewModels;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Manufacturer>> Get(int id)
        {
            try
            {
                var manufacturer = await _manufacturerManager.GetById(id);

                return Ok(manufacturer);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during getting manufacturer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Manufacturer>> Post([FromBody] Manufacturer manufacturer)
        {
            try
            {
                await _manufacturerManager.Add(manufacturer);

                return Ok(manufacturer);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating manufacturer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<Manufacturer>> Put([FromBody] Manufacturer manufacturer)
        {
            try
            {
                await _manufacturerManager.Update(manufacturer);

                return Ok(manufacturer);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing manufacturer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _manufacturerManager.Delete(id);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during deleting manufacturer. Exception: {exception.Message}");
                return Ok();
            }
        }
    }
}