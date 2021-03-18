using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class OrderModel
    {
        public decimal FullCost { set; get; }
        public string Receiver { set; get; }
        public string Status { get; set; }
        public List<ProductModel> GetProducts { get; set; }
    }
}
