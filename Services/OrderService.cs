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
            
            // ТУт втрачаються продукти які ми додавали коли формували замовлення, через те що коли ми переводимо доменну модель до ентіті, в нас втрачаються продукти і вони не доходять до репозиторію
        }

        public Order DeleteOrderByID(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(int id)
        {
            throw new NotImplementedException();
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
