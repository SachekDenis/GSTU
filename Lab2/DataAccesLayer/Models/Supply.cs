using System;

namespace DataAccesLayer.Models
{
    public class Supply : IEntity
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
    }
}