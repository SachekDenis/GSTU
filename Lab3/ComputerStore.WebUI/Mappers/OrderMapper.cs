using System.Collections.Generic;
using System.Linq;
using ComputerStore.BusinessLogicLayer.Models;
using ComputerStore.WebUI.Models;

namespace ComputerStore.WebUI.Mappers
{
    public static class OrderMapper
    {
        public static OrderViewModel CreateOrderViewModel(
            this Order order,
            IEnumerable<Buyer> buyers,
            IEnumerable<Product> products)
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