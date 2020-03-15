using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Ram:Characteristics
    {
        public string Type { get; set; }
        public int Frequency { get; set; }
        public double Volume { get; set; }
        public string Timings { get;set;}

    }
}
