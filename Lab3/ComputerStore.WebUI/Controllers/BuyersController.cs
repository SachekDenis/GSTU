using System;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class BuyersController : Controller
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
        public async Task<IActionResult> Details()
        {
            var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;

            var buyer = await _buyerManager.GetById(buyerId);

            var buyerViewModel = CreateBuyerViewModel(buyer);

            return View(buyerViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BuyerViewModel buyerViewModel)
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

                return RedirectToAction("Index", "Products");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating buyer. Exception: {exception.Message}");
                return View(buyerViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;

            var buyer = await _buyerManager.GetById(buyerId);

            var buyerViewModel = CreateBuyerViewModel(buyer);

            return View(buyerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BuyerViewModel buyerViewModel)
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

                return RedirectToAction("Index", "Products");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating buyer. Exception: {exception.Message}");
                return View(buyerViewModel);
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