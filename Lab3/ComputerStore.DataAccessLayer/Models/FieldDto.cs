namespace ComputerStore.DataAccessLayer.Models
{
    public class FieldDto : IEntity
    {
        public int ProductId { get; set; }
        public int CharacteristicId { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
    }
}