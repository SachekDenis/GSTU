namespace ComputerStore.DataAccessLayer.Models
{
    public class OrderDto : IEntity
    {
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int Amount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int Id { get; set; }
    }
}