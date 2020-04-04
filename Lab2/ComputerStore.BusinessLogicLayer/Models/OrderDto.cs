namespace ComputerStore.BusinessLogicLayer.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int Amount { get; set; }
        public OrderStatusDto OrderStatus { get; set; }
    }
}