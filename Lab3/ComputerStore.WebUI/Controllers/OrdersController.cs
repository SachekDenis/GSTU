using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebUI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BuyerManager _buyerManager;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;

        public OrdersController(ProductManager productManager, BuyerManager buyerManager, OrderManager orderManager)
        {
            _productManager = productManager;
            _buyerManager = buyerManager;
            _orderManager = orderManager;
        }

        public async Task<IActionResult> Index()
        {
            var buyers = await _buyerManager.GetAll();
            var products = await _productManager.GetAll();

            var orderViewModels = (await _orderManager.GetAll()).Select(order => new OrderViewModel
                                                                                 {
                                                                                     Amount = order.Amount,
                                                                                     BuyerId = order.BuyerId,
                                                                                     BuyertName = buyers.First(buyer => buyer.Id == order.BuyerId).FirstName,
                                                                                     OrderStatus = order.OrderStatus,
                                                                                     Id = order.Id,
                                                                                     ProductId = order.ProductId,
                                                                                     ProductName = products.First(product => product.Id == order.ProductId).Name
                                                                                 });
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
            catch
            {
                return View(purchaseViewModel);
            }
        }
    }
}