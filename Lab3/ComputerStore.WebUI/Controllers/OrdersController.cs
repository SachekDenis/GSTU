using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OrdersController : Controller
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;
        private readonly UserManager<IdentityBuyer> _userManager;
        private readonly IMapper _mapper;

        public OrdersController(
            ProductManager productManager,
            BuyerManager buyerManager,
            OrderManager orderManager,
            ILogger<OrdersController> logger,
            UserManager<IdentityBuyer> userManager,
            IMapper mapper)
        {
            _productManager = productManager;
            _buyerManager = buyerManager;
            _orderManager = orderManager;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();
            var orders = await _orderManager.GetAll();

            if (User.IsInRole(RolesNames.Admin))
            {
                var orderViewModels = orders.Select(order => MapOrder(order, buyers, products));
                return View(orderViewModels);
            }
            else
            {
                var buyerId = (await _userManager.GetUserAsync(User)).BuyerId;
                var orderViewModels = orders.Where(order => order.BuyerId == buyerId).Select(order => MapOrder(order, buyers, products));
                return View(orderViewModels);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Buy(int productId)
        {
            var product = await _productManager.GetById(productId);

            var purchaseViewModel = new PurchaseViewModel
                                    {
                                        ProductId = productId,
                                        ProductName = product.Name
                                    };

            return View(purchaseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(PurchaseViewModel purchaseViewModel)
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

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating purchase. Exception: {exception.Message}");
                return View(purchaseViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(
            int id)
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();

            var order = await _orderManager.GetById(id);
            var orderViewModel = MapOrder(order, buyers, products);

            return View(orderViewModel);
        }

        [Authorize(Roles = RolesNames.Admin)]
        [HttpGet]
        public IActionResult Edit(
            int id)
        {
            var orderViewModel = new OrderViewModel
                                 {
                                     Id = id
                                 };

            return View(orderViewModel);
        }

        [Authorize(Roles = RolesNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> Edit(
            OrderViewModel orderViewModel)
        {
            try
            {
                var order = await _orderManager.GetById(orderViewModel.Id);

                order.OrderStatus = orderViewModel.OrderStatus;

                await _orderManager.Update(order);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during editing order. Exception: {exception.Message}");
                return View(orderViewModel);
            }
        }

        private OrderViewModel MapOrder(Order order, IEnumerable<Buyer> buyers, IEnumerable<Product> products)
        {
            return _mapper.Map<Order, OrderViewModel>(order, 
                                                      options => options.AfterMap((src, dest) =>
                                                                                  {
                                                                                      dest.BuyerName = buyers.First(buyer => buyer.Id == order.BuyerId).FirstName;
                                                                                      dest.ProductName = products.First(product => product.Id == order.ProductId).Name;
                                                                                  }));
        }
    }
}