﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class Supplier:Entity
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }
    }
}