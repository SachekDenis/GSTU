namespace ComputerStore.DataAccessLayer.Models
{
    public class SupplierDto : IEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
    }
}