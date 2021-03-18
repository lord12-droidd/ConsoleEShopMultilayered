using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class OrderEntity
    {
        public int ID { set; get; }
        public decimal FullCost { set; get; }
        public string Receiver { set; get; }
        public OrderStatus Status { get; set; }
        public List<ProductEntity> OrderedProducts { get; set; }

    }
}
