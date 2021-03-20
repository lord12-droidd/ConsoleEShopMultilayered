using Data.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private static List<OrderEntity> _orders = new List<OrderEntity>();
        public void AddOrder(OrderEntity order)
        {
            if(_orders.Count == 0)
            {
                order.ID = 0;
                _orders.Add(order);
                return;
            }
            order.ID = _orders[_orders.Count - 1].ID + 1;
            _orders.Add(order);
        }

        public List<OrderEntity> GetOrders()
        {
            List<OrderEntity> users_orderEntities = new List<OrderEntity>();
            for (int i = 0; i < _orders.Count; i++)
            {
                users_orderEntities.Add(_orders[i]);
            }
            return users_orderEntities;
        }

        public void UpdateOrderStatusByCustomID(int id, string currentlogin ,Domain.Enums.OrderStatus orderStatus)
        {
            for(int i = 0; i< GetOrdersByLogin(currentlogin).Count; i++)
            {
                if(id == i)
                {
                    GetOrdersByLogin(currentlogin)[i].Status = orderStatus;

                }
            }
            
        }
        public List<OrderEntity> GetOrdersByLogin(string login)
        {
            List<OrderEntity> users_orderEntities = new List<OrderEntity>();
            for (int i = 0; i < _orders.Count; i++)
            {
                if(login == _orders[i].Receiver)
                {
                    users_orderEntities.Add(_orders[i]);
                }
            }
            return users_orderEntities;
        }
    }
}
