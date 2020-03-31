namespace ComputerStore.BusinessLogicLayer.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int Count { get; set; }
    }
}