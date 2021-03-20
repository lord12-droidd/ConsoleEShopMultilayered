using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Abstract
{
    public interface IOrdersRepository
    {
        void AddOrder(OrderEntity order);
        void UpdateOrderStatusByCustomID(int id, string currentlogin ,Domain.Enums.OrderStatus orderStatus);
        List<OrderEntity> GetOrders();
        List<OrderEntity> GetOrdersByLogin(string login);
    }
}
