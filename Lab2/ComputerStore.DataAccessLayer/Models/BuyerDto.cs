namespace ComputerStore.DataAccessLayer.Models
{
    public class BuyerDto : IEntity
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
    }
}