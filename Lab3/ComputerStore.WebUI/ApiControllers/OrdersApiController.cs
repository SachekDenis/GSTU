using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize(Roles = RolesNames.AdminOrUser, AuthenticationSchemes = JwtInfo.AuthSchemes)]
    public class OrdersApiController : ControllerBase
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<OrdersApiController> _logger;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;
        private readonly UserManager<IdentityBuyer> _userManager;

        public OrdersApiController(
            ProductManager productManager,
            BuyerManager buyerManager,
            OrderManager orderManager,
            ILogger<OrdersApiController> logger,
            UserManager<IdentityBuyer> userManager)
        {
            _productManager = productManager;
            _buyerManager = buyerManager;
            _orderManager = orderManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("orders")]
        public async Task<IEnumerable<OrderViewModel>> Orders()
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();
            var orders = await _orderManager.GetAll();

            if (User.IsInRole(RolesNames.Admin))
            {
                var orderViewModels = orders.Select(order => order.CreateOrderViewModel(buyers, products));
                return orderViewModels;
            }
            else
            {
                var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;
                var orderViewModels = orders.Where(order => order.BuyerId == buyerId).Select(order => order.CreateOrderViewModel(buyers, products));
                return orderViewModels;
            }
        }

        [HttpPost("Buy")]
        public async Task<StatusCodeResult> Buy([FromBody] PurchaseViewModel purchaseViewModel)
        {
            try
            {
                var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;

                var buyer = await _buyerManager.GetById(buyerId);

                var order = new Order
                            {
                                Amount = purchaseViewModel.Amount,
                                BuyerId = buyer.Id,
                                ProductId = purchaseViewModel.ProductId
                            };

                var product = await _productManager.GetById(order.ProductId);
                product.AmountInStorage -= order.Amount;

                await _productManager.Update(product);

                await _orderManager.Add(order);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating purchase. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [HttpGet("details")]
        public async Task<OrderViewModel> Details(
            int id)
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();

            var order = await _orderManager.GetById(id);
            var orderViewModel = order.CreateOrderViewModel(buyers, products);

            return orderViewModel;
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPost("edit")]
        public async Task<StatusCodeResult> Edit(OrderViewModel orderViewModel)
        {
            try
            {
                var order = await _orderManager.GetById(orderViewModel.Id);

                order.OrderStatus = orderViewModel.OrderStatus;

                await _orderManager.Update(order);

                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing order. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}