using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Manufacturer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}