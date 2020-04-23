using System.ComponentModel.DataAnnotations;
using ComputerStore.BusinessLogicLayer.Validation.RegexStorage;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Supplier
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(RegexCollection.PhoneRegex)]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
    }
}