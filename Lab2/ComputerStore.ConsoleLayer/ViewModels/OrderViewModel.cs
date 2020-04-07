using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.ConsoleLayer.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string BuyerAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int Count { get; set; }
    }
}