namespace DataAccesLayer.Models
{
    public class Manufacturer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }
    }
}
