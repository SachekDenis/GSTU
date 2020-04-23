namespace ComputerStore.DataAccessLayer.Models
{
    public class ManufacturerDto : IEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Id { get; set; }
    }
}