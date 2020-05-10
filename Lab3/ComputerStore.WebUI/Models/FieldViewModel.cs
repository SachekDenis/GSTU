using System.ComponentModel.DataAnnotations;

namespace ComputerStore.WebUI.Models
{
    public class FieldViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CharacteristicId { get; set; }

        [Required]
        public string CharacteristicName { get; set; }

        [Required]
        public string Value { get; set; }
    }
}