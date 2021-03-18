using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    class Outputer
    {
        public void OutputProducts(List<Domain.Product> products)
        {
            for(int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"Product name: {products[i].Name}");
                Console.WriteLine($"Category: {products[i].Category}");
                Console.WriteLine($"Description: {products[i].Description}");
                Console.WriteLine($"Product cost: {products[i].Cost}");
                Console.WriteLine($"Product CODE: {products[i].CodeProduct}");
                Console.WriteLine("---------------------------------------------------------------");
            }
            if(products.Count == 0)
            {
                Console.WriteLine("There is no products");
            }
        }
        public void OutputUsersOrders(List<Domain.Order> orders)
        {
            for(int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine($"Order number {i+1}");
                Console.WriteLine("");
                OutputProducts(orders[i].OrderedProducts);
                Console.WriteLine($"Order status: {orders[i].Status}");
                Console.WriteLine($"Full cost: {orders[i].FullCost}");
            }
            if(orders.Count == 0)
            {
                Console.WriteLine("No orders");
            }
        }
        public void OutputUserInfo(Domain.RegistredUser registredUser)
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine($"{registredUser.Login} info");
            Console.WriteLine($"You can`t change login and Email");
            Console.WriteLine($"Name: {registredUser.Name}");
            Console.WriteLine($"Lastname: {registredUser.Lastname}");
            Console.WriteLine($"Email: {registredUser.Email}");
            Console.WriteLine($"Login: {registredUser.Login}");
            Console.WriteLine($"Status: {registredUser.Status}");
            Console.WriteLine("---------------------------------------------------------------");
        }
        public void OutputUsersInfo(List<Domain.RegistredUser> registredUsers)
        {
            for(int i = 0; i < registredUsers.Count; i++)
            {
                OutputUserInfo(registredUsers[i]);
            }
        }
        public void OutputUsersLogins(List<Domain.RegistredUser> registredUsers)
        {
            for(int i = 0; i < registredUsers.Count; i++)
            {
                Console.WriteLine($"Login: {registredUsers[i].Login}");
                Console.WriteLine("---------------------------------------------------------------");
            }
        }
    }
}
