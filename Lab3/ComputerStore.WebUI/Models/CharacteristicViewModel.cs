using System.ComponentModel.DataAnnotations;

namespace ComputerStore.WebUI.Models
{
    public class CharacteristicViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string Name { get; set; }
    }
}