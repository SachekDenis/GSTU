using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class GraphicalCard : Characteristics
    {
        public string GraphicalProcessor { get; set; }

        public double GpuFrequency { get; set; }

        public double VramSize { get; set; }

        public string VramType { get; set; }

        public double VramFrequency { get; set; }
    }
}
