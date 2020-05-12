using System.ComponentModel.DataAnnotations;
using ComputerStore.BusinessLogicLayer.Validation.RegexStorage;

namespace ComputerStore.WebUI.Models
{
    public class PurchaseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}