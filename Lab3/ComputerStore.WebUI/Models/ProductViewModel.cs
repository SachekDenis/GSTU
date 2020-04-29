using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ComputerStore.BusinessLogicLayer.Models;

namespace ComputerStore.WebUI.Models
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AmountInStorage { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public string ManufacturerName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public IEnumerable<FieldViewModel> Fields { get; set; }
    }
}