using System.ComponentModel.DataAnnotations;
using ComputerStore.BusinessLogicLayer.Validation.RegexStorage;

namespace ComputerStore.WebUI.Models
{
    public class PurchaseViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(RegexCollection.PhoneRegex)]
        public string PhoneNumber { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}