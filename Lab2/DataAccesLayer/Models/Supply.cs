using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Supply:Entity
    {
        public int SupplierId { get; set; }
        public Supplier Supplier {get;set;}
        public DateTime Date { get; set; }
    }
}
