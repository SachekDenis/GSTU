using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebUI.Controllers;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/buyers")]
    [ApiController]
    public class BuyersApiController : ControllerBase
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<BuyersController> _logger;
        private readonly UserManager<IdentityBuyer> _userManager;

        public BuyersApiController(BuyerManager buyerManager, ILogger<BuyersController> logger, UserManager<IdentityBuyer> userManager)
        {
            _buyerManager = buyerManager;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/BuyersApi/Details
        [HttpGet("Details")]
        public async Task<BuyerViewModel> Details()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return null;
            }

            var buyerId = user.BuyerId;

            var buyer = await _buyerManager.GetById(buyerId);

            var buyerViewModel = CreateBuyerViewModel(buyer);

            return buyerViewModel;
        }

        [HttpPost("Create")]
        public async Task Create([FromBody] BuyerViewModel buyerViewModel)
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

                var user = await _userManager.GetUserAsync(User);

                user.BuyerId = buyer.Id;

                await _userManager.UpdateAsync(user);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating buyer. Exception: {exception.Message}");
            }
        }

        [HttpPost("Edit")]
        public async Task Edit([FromBody] BuyerViewModel buyerViewModel)
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
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating buyer. Exception: {exception.Message}");
            }
        }

        private BuyerViewModel CreateBuyerViewModel(Buyer buyer)
        {
            return new BuyerViewModel
                   {
                       Id = buyer.Id,
                       Address = buyer.Address,
                       Email = buyer.Email,
                       FirstName = buyer.FirstName,
                       PhoneNumber = buyer.PhoneNumber,
                       SecondName = buyer.SecondName,
                       ZipCode = buyer.ZipCode
                   };
        }
    }
}