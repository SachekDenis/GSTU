using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Supply
    {
        public int Id { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime Date { get; set; }
    }
}
