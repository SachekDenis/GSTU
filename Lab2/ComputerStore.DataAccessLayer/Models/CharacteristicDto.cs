namespace ComputerStore.DataAccessLayer.Models
{
    public class CharacteristicDto : IEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}