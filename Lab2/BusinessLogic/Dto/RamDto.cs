using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dto
{
    public class RamDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int ManufacturerId { get; set; }
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }
        public string Type { get; set; }
        public int Frequency { get; set; }
        public double Volume { get; set; }
        public string Timings { get; set; }


    }
}
