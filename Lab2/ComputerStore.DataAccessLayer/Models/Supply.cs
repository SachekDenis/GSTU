using System;

namespace ComputerStore.DataAccessLayer.Models
{
    public class Supply : IEntity
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}