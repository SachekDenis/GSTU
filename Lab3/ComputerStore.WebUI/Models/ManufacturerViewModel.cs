using System.ComponentModel.DataAnnotations;

namespace ComputerStore.WebUI.Models
{
    public class ManufacturerViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}