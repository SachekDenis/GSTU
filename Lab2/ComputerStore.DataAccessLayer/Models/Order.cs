namespace ComputerStore.DataAccessLayer.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int Amount { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}