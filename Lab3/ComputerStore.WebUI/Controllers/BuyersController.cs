using System;
using System.Threading.Tasks;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebUI.AppConfiguration;
using ComputerStore.WebUI.Mappers;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    [Authorize(Roles = RolesNames.AdminOrUser)]
    public class BuyersController : Controller
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<BuyersController> _logger;
        private readonly UserManager<IdentityBuyer> _userManager;
        private readonly IMapper _mapper;

        public BuyersController(
            BuyerManager buyerManager, 
            ILogger<BuyersController> logger,
            UserManager<IdentityBuyer> userManager,
            IMapper mapper)
        {
            _buyerManager = buyerManager;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;

            var buyer = await _buyerManager.GetById(buyerId);

            var buyerViewModel = _mapper.Map<Buyer, BuyerViewModel>(buyer);

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
                var buyer = _mapper.Map<BuyerViewModel, Buyer>(buyerViewModel);

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

            var buyerViewModel = _mapper.Map<Buyer, BuyerViewModel>(buyer);

            return View(buyerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BuyerViewModel buyerViewModel)
        {
            try
            {
                var buyer = _mapper.Map<BuyerViewModel, Buyer>(buyerViewModel);

                await _buyerManager.Update(buyer);

                return RedirectToAction("Index", "Products");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during updating buyer. Exception: {exception.Message}");
                return View(buyerViewModel);
            }
        }
    }
}