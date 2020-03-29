namespace DataAccesLayer.Models
{
    public class Supplier : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
