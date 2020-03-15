using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Motherboard:Characteristics
    {
        public string Socket { get; set; }

        public string Chipset { get; set; }

        public string RamType { get; set; }

        public int RamCount { get; set; }

        public string ExtensionSlots { get; set; }

        public string Audio { get; set; }

        public string Ethernet { get; set; }

        public string Interfaces { get; set; }
    }
}
