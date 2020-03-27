namespace DataAccesLayer.Models
{
    public class Field : Entity
    {
        public int ProductId { get; set; }
        public int CharacteristicId { get; set; }
        public string Value { get; set; }
    }
}