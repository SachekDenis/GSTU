using System.Collections.Generic;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AmountInStorage { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Field> Fields { get; set; }
    }
}