using System.Collections.Generic;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CountInStorage { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public List<FieldDto> Fields { get; set; }
    }
}