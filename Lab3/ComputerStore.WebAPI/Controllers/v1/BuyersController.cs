using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebAPI.Models;
using ComputerStore.WebUI.AppConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebAPI.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = RolesNames.AdminOrUser, AuthenticationSchemes = JwtInfo.AuthSchemes)]
    public class BuyersController : ControllerBase
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<BuyersController> _logger;
        private readonly UserManager<IdentityBuyer> _userManager;

        public BuyersController(BuyerManager buyerManager,
            ILogger<BuyersController> logger,
            UserManager<IdentityBuyer> userManager)
        {
            _buyerManager = buyerManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<Buyer>> Get()
        {
            try
            {
                var buyerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == BuyerClaim.BuyerId).Value);

                var buyer = await _buyerManager.GetById(buyerId);

                return buyer;
            }
            catch (Exception exception)
            {
               _logger.LogError($"Error occured during get buyer. Exception: {exception.Message}");
               return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Buyer>> Post([FromBody] Buyer buyer)
        {
            try
            {
                await _buyerManager.Add(buyer);

                var user = await _userManager.FindByEmailAsync(User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value);

                user.BuyerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == BuyerClaim.BuyerId).Value);

                await _userManager.UpdateAsync(user);
                
                return Ok(buyer);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating buyer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<Buyer>> Put([FromBody] Buyer buyer)
        {
            try
            {
                await _buyerManager.Update(buyer);

                return Ok(buyer);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating buyer. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}