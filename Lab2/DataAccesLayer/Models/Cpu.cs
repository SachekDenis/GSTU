using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Cpu:Characteristics
    {
        public double Frequency { get; set; }

        public string Socket { get; set; }

        public string Core { get; set; }

        public int NumberOfCores { get; set; }

        public string ProcessTechnology { get; set; }
    }
}
