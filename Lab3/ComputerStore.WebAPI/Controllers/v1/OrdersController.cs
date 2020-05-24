using System;
using System.Collections.Generic;
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
    public class OrdersController : ControllerBase
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;
        private readonly UserManager<IdentityBuyer> _userManager;

        public OrdersController(
            ProductManager productManager,
            BuyerManager buyerManager,
            OrderManager orderManager,
            ILogger<OrdersController> logger,
            UserManager<IdentityBuyer> userManager)
        {
            _productManager = productManager;
            _buyerManager = buyerManager;
            _orderManager = orderManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = (await _orderManager.GetAll());

            if (User.IsInRole(RolesNames.Admin))
            {
                return orders.ToList();
            }
            else
            {
                var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;
                return orders.Where(order => order.BuyerId == buyerId).ToList();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Buy([FromBody] Purchase purchase)
        {
            try
            {
                var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;

                var buyer = await _buyerManager.GetById(buyerId);

                var order = new Order
                            {
                                Amount = purchase.Amount,
                                BuyerId = buyer.Id,
                                ProductId = purchase.ProductId
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            try
            {
                var order = await _orderManager.GetById(id);

                return order;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during getting order. Exception: {exception.Message}");
                return BadRequest();
            }
        }

        [Authorize(Roles = RolesNames.Admin, AuthenticationSchemes = JwtInfo.AuthSchemes)]
        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            try
            {
                await _orderManager.Update(order);

                return Ok(order);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing order. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}