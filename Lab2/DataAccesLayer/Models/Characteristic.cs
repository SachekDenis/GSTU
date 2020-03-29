namespace DataAccessLayer.Models
{
    public class Characteristic : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
