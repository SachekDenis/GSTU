using System.ComponentModel.DataAnnotations;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.WebUI.Models
{
    public class OrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required]
        public int BuyerId { get; set; }

        public string BuyertName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}