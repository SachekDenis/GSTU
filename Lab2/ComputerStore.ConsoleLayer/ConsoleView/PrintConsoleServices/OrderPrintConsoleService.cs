using System.Linq;
using System.Threading.Tasks;
using ComputerStore.BusinessLogicLayer.Managers;
using ComputerStore.ConsoleLayer.ViewModels;

namespace ComputerStore.ConsoleLayer.ConsoleView.PrintConsoleServices
{
    public class OrderPrintConsoleService : IPrintConsoleService
    {
        private readonly BuyerManager _buyerManager;
        private readonly OrderManager _orderManager;
        private readonly ProductManager _productManager;

        public OrderPrintConsoleService(OrderManager orderManager, ProductManager productManager, BuyerManager buyerManager)
        {
            _orderManager = orderManager;
            _productManager = productManager;
            _buyerManager = buyerManager;
        }

        public async Task PrintAll()
        {
            var products = await _productManager.GetAll();
            var buyers = await _buyerManager.GetAll();

            var items = (await _orderManager.GetAll()).Select(item => new OrderViewModel
                                                                      {
                                                                          ProductName = products.First(product => product.Id == item.ProductId).Name,
                                                                          BuyerAddress = buyers.First(buyer => buyer.Id == item.BuyerId).Address,
                                                                          Count = item.Amount,
                                                                          OrderStatus = item.OrderStatus,
                                                                          Id = item.Id
                                                                      });

            items.WriteCollectionAsTable();
        }
    }
}