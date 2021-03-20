using Data.Repositories.Abstract;
using Domain;
using Mappers;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrderService(IOrdersRepository orderRepository)
        {
            _ordersRepository = orderRepository;
        }

        Data.Repositories.OrdersRepository ordersRepository = new Data.Repositories.OrdersRepository();
        public void AddOrder(Order order)
        {
            ordersRepository.AddOrder(order.ToEntity());
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            for (int i = 0; i < ordersRepository.GetOrders().Count; i++)
            {
                orders.Add(OrderMapper.ToDomain(ordersRepository.GetOrders()[i]));
            }
            return orders;
        }

        public void UpdateOrderStatusByCustomID(int id, string currentlogin ,Domain.Enums.OrderStatus orderStatus)
        {
            ordersRepository.UpdateOrderStatusByCustomID(id,currentlogin ,orderStatus);
        }

        public List<Order> GetOrdersByLogin(string login)
        {
            List<Order> orders = new List<Order>();
            for (int i = 0; i < ordersRepository.GetOrdersByLogin(login).Count; i++)
            {
                orders.Add(OrderMapper.ToDomain(ordersRepository.GetOrdersByLogin(login)[i]));
            }
            return orders;

        }
    }
}
