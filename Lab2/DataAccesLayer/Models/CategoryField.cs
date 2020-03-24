using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccesLayer.Models
{
    public class CategoryField
    {
        public int CategoryId { get;set;}
        public Category Category { get;set;}
        public int FieldId { get;set;}
        public Field Field{get;set;}
    }
}
