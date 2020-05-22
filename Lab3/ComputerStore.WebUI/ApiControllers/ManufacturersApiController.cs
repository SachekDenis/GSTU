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
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/manufacturers")]
    [ApiController]
    [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
    public class ManufacturersApiController : ControllerBase
    {
        private readonly ILogger<ManufacturersApiController> _logger;
        private readonly ManufacturerManager _manufacturerManager;

        public ManufacturersApiController(ManufacturerManager manufacturerManager,
                                         ILogger<ManufacturersApiController> logger)
        {
            _manufacturerManager = manufacturerManager;
            _logger = logger;
        }

        [HttpGet("manufacturers")]
        public async Task<IEnumerable<ManufacturerViewModel>> Manufacturers()
        {
            var manufacturerViewModels = (await _manufacturerManager.GetAll()).Select(manufacturer => manufacturer.CreateManufacturerViewModel());

            return manufacturerViewModels;
        }
        
        [HttpGet("details")]
        public async Task<ManufacturerViewModel> Details([FromQuery] int id)
        {
            var manufacturer = await _manufacturerManager.GetById(id);

            var manufacturerViewModel = manufacturer.CreateManufacturerViewModel();

            return manufacturerViewModel;
        }

        [HttpPost("create")]
        public async Task<StatusCodeResult> Create([FromBody] ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                await _manufacturerManager.Add(new Manufacturer
                                               {
                                                   Country = manufacturerViewModel.Country,
                                                   Id = manufacturerViewModel.Id,
                                                   Name = manufacturerViewModel.Name
                                               });

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating manufacturer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        // POST: Manufacturers/Edit/5
        [HttpPost("edit")]
        public async Task<StatusCodeResult> Edit([FromBody] ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                await _manufacturerManager.Update(new Manufacturer
                                                  {
                                                      Country = manufacturerViewModel.Country,
                                                      Id = manufacturerViewModel.Id,
                                                      Name = manufacturerViewModel.Name
                                                  });

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing manufacturer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPost("delete")]
        public async Task<StatusCodeResult> Delete([FromBody] ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                await _manufacturerManager.Delete(manufacturerViewModel.Id);

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