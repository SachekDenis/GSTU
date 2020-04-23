using System.ComponentModel.DataAnnotations;
using ComputerStore.BusinessLogicLayer.Validation;
using ComputerStore.BusinessLogicLayer.Validation.RegexStorage;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Buyer
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
    }
}