using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Characteristic
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}