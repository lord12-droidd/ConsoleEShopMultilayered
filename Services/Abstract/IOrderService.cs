using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        void UpdateOrderStatusByCustomID(int id, string currentlogin ,Domain.Enums.OrderStatus orderStatus);
        Order GetOrderByID(int id);
        List<Order> GetOrders();
        Order DeleteOrderByID(int id);
        List<Order> GetOrdersByLogin(string login);
    }
}
