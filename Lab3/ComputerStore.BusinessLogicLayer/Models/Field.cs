using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Field
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CharacteristicId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}