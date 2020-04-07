using System;

namespace ComputerStore.BusinessLogicLayer.Models
{
    public class Supply
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}