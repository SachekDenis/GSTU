using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Supply
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}