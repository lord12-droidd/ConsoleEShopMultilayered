using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
  
    public class ProductEntity
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public string Description { set; get; }
        public decimal Cost { set; get; }
        public string CodeProduct { set; get; }
    }
}
