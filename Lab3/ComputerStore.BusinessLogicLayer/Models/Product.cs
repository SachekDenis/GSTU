using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "5.0", "99999.0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AmountInStorage { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IEnumerable<Field> Fields { get; set; }
    }
}