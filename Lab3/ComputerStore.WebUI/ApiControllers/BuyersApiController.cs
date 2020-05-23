using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebUI.AppConfiguration;
using ComputerStore.WebUI.Controllers;
using ComputerStore.WebUI.Mappers;
using ComputerStore.WebUI.Models;
using ComputerStore.WebUI.Models.JwtToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/buyers")]
    [ApiController]
    [Authorize(Roles = RolesNames.AdminOrUser, AuthenticationSchemes = JwtInfo.AuthSchemes)]
    public class BuyersApiController : ControllerBase
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<BuyersApiController> _logger;
        private readonly UserManager<IdentityBuyer> _userManager;

        public BuyersApiController(BuyerManager buyerManager,
            ILogger<BuyersApiController> logger,
            UserManager<IdentityBuyer> userManager)
        {
            _buyerManager = buyerManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("details")]
        public async Task<BuyerViewModel> Details()
        {
            var buyerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == BuyerClaim.BuyerId).Value);

            if (buyerId == 0)
                return null;

            var buyer = await _buyerManager.GetById(buyerId);

            var buyerViewModel = buyer.CreateBuyerViewModel();

            return buyerViewModel;
        }

        [HttpPost("create")]
        public async Task<StatusCodeResult> Create([FromBody] BuyerViewModel buyerViewModel)
        {
            try
            {
                var buyer = new Buyer
                            {
                                Address = buyerViewModel.Address,
                                Email = buyerViewModel.Email,
                                FirstName = buyerViewModel.FirstName,
                                PhoneNumber = buyerViewModel.PhoneNumber,
                                SecondName = buyerViewModel.SecondName,
                                ZipCode = buyerViewModel.ZipCode
                            };

                await _buyerManager.Add(buyer);

                var user = await _userManager.FindByEmailAsync(User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value);

                user.BuyerId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == BuyerClaim.BuyerId).Value);

                await _userManager.UpdateAsync(user);
                
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating buyer. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpPost("edit")]
        public async Task<StatusCodeResult> Edit([FromBody] BuyerViewModel buyerViewModel)
        {
            try
            {
                var buyer = new Buyer
                            {
                                Id = buyerViewModel.Id,
                                Address = buyerViewModel.Address,
                                Email = buyerViewModel.Email,
                                FirstName = buyerViewModel.FirstName,
                                PhoneNumber = buyerViewModel.PhoneNumber,
                                SecondName = buyerViewModel.SecondName,
                                ZipCode = buyerViewModel.ZipCode
                            };

                await _buyerManager.Update(buyer);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating buyer. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}