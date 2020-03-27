using System;

namespace DataAccesLayer.Models
{
    public class Supply : Entity
    {
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
    }
}