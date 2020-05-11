﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BuyerManager _buyerManager;
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;

        public OrdersController(ProductManager productManager, BuyerManager buyerManager, OrderManager orderManager, ILogger<OrdersController> logger)
        {
            _productManager = productManager;
            _buyerManager = buyerManager;
            _orderManager = orderManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();

            var orderViewModels = (await _orderManager.GetAll()).Select(order => CreateOrderViewModel(order, buyers, products));
            return View(orderViewModels);
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
                var buyer = new Buyer
                            {
                                Address = purchaseViewModel.Address,
                                Email = purchaseViewModel.Email,
                                FirstName = purchaseViewModel.FirstName,
                                PhoneNumber = purchaseViewModel.PhoneNumber,
                                SecondName = purchaseViewModel.SecondName,
                                ZipCode = purchaseViewModel.ZipCode
                            };

                await _buyerManager.Add(buyer);

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
        public async Task<IActionResult> Details(int id)
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();

            var order = await _orderManager.GetById(id);
            var orderViewModel = CreateOrderViewModel(order, buyers, products);

            return View(orderViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orderViewModel = new OrderViewModel
                                 {
                                     Id = id
                                 };

            return View(orderViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel orderViewModel)
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

        private OrderViewModel CreateOrderViewModel(Order order, IEnumerable<Buyer> buyers, IEnumerable<Product> products)
        {
            return new OrderViewModel
                   {
                       Amount = order.Amount,
                       BuyerId = order.BuyerId,
                       BuyerName = buyers.First(buyer => buyer.Id == order.BuyerId).FirstName,
                       OrderStatus = order.OrderStatus,
                       Id = order.Id,
                       ProductId = order.ProductId,
                       ProductName = products.First(product => product.Id == order.ProductId).Name
                   };
        }
    }
}