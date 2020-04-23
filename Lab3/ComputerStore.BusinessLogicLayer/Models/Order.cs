using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}