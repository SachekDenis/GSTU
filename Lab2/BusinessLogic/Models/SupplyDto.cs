using System;

namespace BusinessLogic.Models
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
