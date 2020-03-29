namespace DataAccesLayer.Models
{
    public class Field : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CharacteristicId { get; set; }
        public string Value { get; set; }
    }
}