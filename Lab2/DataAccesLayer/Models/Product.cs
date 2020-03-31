namespace DataAccessLayer.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CountInStorage { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
    }
}