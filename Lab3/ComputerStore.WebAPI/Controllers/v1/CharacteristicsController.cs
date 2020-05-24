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
    public class CharacteristicsController : ControllerBase
    {
        private readonly CharacteristicManager _characteristicManager;
        private readonly ILogger<CharacteristicsController> _logger;

        public CharacteristicsController(CharacteristicManager characteristicManager,
                                         ILogger<CharacteristicsController> logger)
        {
            _characteristicManager = characteristicManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Characteristic>>> Get()
        {
            var characteristics = (await _characteristicManager.GetAll()).ToList();

            return characteristics;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Characteristic>> Get(int id)
        {
            try
            {
                var characteristic = await _characteristicManager.GetById(id);
                return Ok(characteristic);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during get characteristic. Exception: {exception.Message}");
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<ActionResult<Characteristic>> Post([FromBody] Characteristic characteristic)
        {
            try
            {
                await _characteristicManager.Add(characteristic);

                return Ok(characteristic);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating characteristic. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<Characteristic>> Put([FromBody] Characteristic characteristic)
        {
            try
            {
                await _characteristicManager.Update(characteristic);

                return Ok(characteristic);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing characteristic. Exception: {exception.Message}");
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _characteristicManager.Delete(id);

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
