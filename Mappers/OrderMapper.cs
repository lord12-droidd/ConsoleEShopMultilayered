using Domain;
using Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mappers
{
    public static class OrderMapper
    {
        public static OrderEntity ToEntity(this Order order)
        {
            return new OrderEntity()
            {
                Receiver = order.Receiver,
                FullCost = order.FullCost,
                Status = order.Status,
                OrderedProducts = order.OrderedProducts.Select(productDomain => productDomain.ToEntity()).ToList()
            };
        }
        public static OrderEntity ToEntityWithUserOrderID(this Order order, int id)
        {
            return new OrderEntity()
            {
                ID = id,
                Receiver = order.Receiver,
                FullCost = order.FullCost,
                Status = order.Status,
                OrderedProducts = order.OrderedProducts.Select(productDomain => productDomain.ToEntity()).ToList()
            };
        }
        public static Order ToDomain(this OrderEntity order)
        {
            if (order == null) return null;
            return new Order
            {
                FullCost = order.FullCost,
                Receiver = order.Receiver,
                Status = order.Status,
                OrderedProducts = order.OrderedProducts.Select(product => product.ToDomain()).ToList()
            };
        }
    }
}
