using System;

namespace ComputerStore.DataAccessLayer.Models
{
    public class SupplyDto : IEntity
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
    }
}