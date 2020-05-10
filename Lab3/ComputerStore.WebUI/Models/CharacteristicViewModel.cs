using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public SelectList CategoriesSelectList { get; set; }
    }
}