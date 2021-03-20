using Data.Repositories.Abstract;
using Data.Repositories;
using Services;
using Services.Abstract;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UI
{
    public class Shop
    {
        private  IRegistredUsersRepository _registredUsersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        public void Start()
        {
            CurrentUser current = new CurrentUser(Rights.Guest);
            Checker checker = new Checker();
            Outputer outputer = new Outputer();
            Inputer inputer = new Inputer();
            while (true)
            {
                RegistredUserService registredUserService = new RegistredUserService(_registredUsersRepository);  // ми не можемо викликати методи просто через інтерфейс
                ProductService productService = new ProductService(_productsRepository);
                OrderService ordersService = new OrderService(_ordersRepository);
                Console.WriteLine();
                ShowMenu(current);
                string choice = Console.ReadLine();
                Console.WriteLine();
                
                if (current.CurrentRights == Rights.Guest)
                {
                    switch (choice)
                    {
                        case "0":
                            outputer.OutputProducts(productService.GetProducts());
                            break;
                        case "1":
                            outputer.OutputProducts(productService.GetProductsByName(Console.ReadLine()));
                            break;
                        case "2":
                            Domain.RegistredUser registredUser = new Domain.RegistredUser
                            {
                                Name = checker.CheckIsNotEmpty(inputer.InputName()),
                                Lastname = checker.CheckIsNotEmpty(inputer.InputSurname()),
                                Email = checker.CheckIsNotEmpty(inputer.InputEmail()),
                                Login = checker.CheckIsNotEmpty(inputer.InputLogin()),
                                Password = checker.CheckIsNotEmpty(inputer.InputPassword())
                            };
                            if (registredUserService.AddUser(registredUser) == true)
                            {
                                Console.WriteLine("Registration finished successfully");
                                break;
                            }
                            Console.WriteLine("User with such login or email already exists");
                            break;
                        case "3":
                            string login = checker.CheckIsNotEmpty(inputer.InputLogin());
                            string password = checker.CheckIsNotEmpty(inputer.InputPassword());
                            if(registredUserService.GetUserByLogin(login).Login == login 
                                && registredUserService.GetUserByLogin(login).Password == password)
                            {
                                Console.WriteLine("Successfull enter");
                                current.Login = registredUserService.GetUserByLogin(login).Login;
                                if ((int)registredUserService.GetUserByLogin(login).Status == (int)Rights.Admin)
                                {
                                    current.CurrentRights = Rights.Admin;
                                    break;
                                }
                                current.CurrentRights = Rights.RegistredUser;
                                break;
                            }
                            Console.WriteLine("Invalid login or password");
                            break;
                    }
                }
                else if (current.CurrentRights == Rights.RegistredUser)
                {
                    switch (choice)
                    {
                        case "0":
                            outputer.OutputProducts(productService.GetProducts());
                            break;
                        case "1":
                            outputer.OutputProducts(productService.GetProductsByName(Console.ReadLine()));
                            break;
                        case "2":
                            List<string> bucket = new List<string>();
                            bucket = inputer.FormBucket(productService.GetProducts());
                            if(bucket.Count == 0)
                            {
                                break;
                            }
                            Domain.Order order = new Domain.Order() 
                            {
                                OrderedProducts = productService.GetBucketProducts(bucket),
                                Receiver = registredUserService.GetUserByLogin(current.Login).Login,
                                Status = Domain.Enums.OrderStatus.New,
                                FullCost = productService.GetBucketProducts(bucket).Sum(product => product.Cost)
                            };


                            //order.OrderedProducts.AddRange(productService.GetBucketProducts(inputer.FormBucket(productService.GetProducts())));
                            //order.Receiver = registredUserService.GetUserByLogin(current.Login).Login;
                            ordersService.AddOrder(order);
                            Console.WriteLine();
                            break;
                        case "3":
                            //(current as RegistredGuest).OrderRegistration((current as RegistredGuest).Login);
                            //outputer.OutputUsersOrders(ordersService.GetOrdersByLogin(current.Login));
                            if(ordersService.GetOrdersByLogin(current.Login).Count == 0)
                            {
                                Console.WriteLine("Your backet is an empty");
                                break;
                            }
                            ordersService.UpdateOrderStatusByCustomID(inputer.InputOrderIDToUpdate(ordersService.GetOrdersByLogin(current.Login)),current.Login ,Domain.Enums.OrderStatus.UserDeny);
                            Console.WriteLine();
                            break;
                        case "4":
                            outputer.OutputUsersOrders(ordersService.GetOrdersByLogin(current.Login));
                            Console.WriteLine();
                            break;
                        case "5":
                            if (ordersService.GetOrdersByLogin(current.Login).Count == 0)
                            {
                                Console.WriteLine("Your backet is an empty");
                                break;
                            }
                            ordersService.UpdateOrderStatusByCustomID(inputer.InputOrderIDToUpdate(ordersService.GetOrdersByLogin(current.Login)),current.Login ,Domain.Enums.OrderStatus.Received);
                            Console.WriteLine();
                            break;
                        case "6":
                            outputer.OutputUserInfo(registredUserService.GetUserByLogin(current.Login));
                            registredUserService.UpdateUser(inputer.InputNewInfo(registredUserService.GetUserByLogin(current.Login)));
                            Console.WriteLine();
                            break;
                        case "7":
                            current.CurrentRights = Rights.Guest;
                            break;
                    }
                }
                else if (current.CurrentRights == Rights.Admin)
                {
                    switch (choice)
                    {
                        case "0":
                            outputer.OutputProducts(productService.GetProducts());
                            break;
                        case "1":
                            outputer.OutputProducts(productService.GetProductsByName(Console.ReadLine()));
                            break;
                        case "2":
                            List<string> bucket = new List<string>();
                            bucket = inputer.FormBucket(productService.GetProducts());
                            if (bucket.Count == 0)
                            {
                                break;
                            }
                            Domain.Order order = new Domain.Order()
                            {
                                OrderedProducts = productService.GetBucketProducts(bucket),
                                Receiver = registredUserService.GetUserByLogin(current.Login).Login,
                                Status = Domain.Enums.OrderStatus.New,
                                FullCost = productService.GetBucketProducts(bucket).Sum(product => product.Cost)
                            };
                            ordersService.AddOrder(order);
                            Console.WriteLine();
                            break;
                        case "3":
                            outputer.OutputUsersOrders(ordersService.GetOrdersByLogin(current.Login));
                            if (ordersService.GetOrdersByLogin(current.Login).Count == 0)
                            {
                                Console.WriteLine("Your backet is an empty");
                                break;
                            }
                            ordersService.UpdateOrderStatusByCustomID(inputer.InputOrderIDToUpdate(ordersService.GetOrdersByLogin(current.Login)), current.Login, Domain.Enums.OrderStatus.UserDeny);
                            Console.WriteLine();
                            break;
                        case "4":
                            registredUserService.GetRegistredUsers();
                            outputer.OutputUsersInfo(registredUserService.GetRegistredUsers());
                            if(inputer.IsAdminWantToChange() == true)
                            {
                                string login = checker.CheckIsNotEmpty(inputer.InputLogin());
                                if (checker.IsUserExists(registredUserService.GetRegistredUsers(), login))
                                {
                                    registredUserService.UpdateUser(inputer.InputNewInfo(registredUserService.GetUserByLogin(login)));
                                }
                            }
                            Console.WriteLine();
                            break;
                        case "5":
                            Domain.Product newProduct = new Domain.Product
                            {
                                Name = checker.CheckIsNotEmpty(inputer.InputProductName()),
                                Category = checker.CheckIsNotEmpty(inputer.InputProductCategory()),
                                Cost = checker.IsFieldDecimal(inputer.InputProductCost()),
                                Description = checker.CheckIsNotEmpty(inputer.InputProductDescription()),
                                CodeProduct = checker.CheckIsNotEmpty(inputer.InputProductCode())
                            };
                            if(productService.AddProduct(newProduct) == true)
                            {
                                Console.WriteLine("Product was added successfully");
                                Console.WriteLine();
                                break;
                            }
                            Console.WriteLine("Product with such product code already exists");
                            Console.WriteLine();
                            break;
                        case "6":
                            //(current as Admin).ViewProductsList();
                            outputer.OutputProducts(productService.GetProducts());
                            Console.WriteLine();
                            if(inputer.IsAdminWantToChange() == true)
                            {
                                string productCode = checker.CheckIsNotEmpty(inputer.InputProductCode());
                                if (checker.IsProductCodeExists(productService.GetProducts(), productCode))
                                {
                                    Domain.Product updateedProduct = inputer.InputNewProductInfo(productService.GetProductByCode(productCode));
                                    if (productService.SearchProductByCODE(updateedProduct.CodeProduct) && updateedProduct.CodeProduct != productCode)
                                    {
                                        Console.WriteLine("Product with such product code already exists");
                                        break;
                                    }
                                    productService.UpdateProduct(updateedProduct, productCode);
                                    Console.WriteLine("Product was updated successfully");
                                }
                            }
                            Console.WriteLine();
                            break;
                        case "7":
                            outputer.OutputUsersLogins(registredUserService.GetRegistredUsers());
                            string chosenlogin = checker.CheckIsNotEmpty(inputer.InputLogin());
                            ordersService.UpdateOrderStatusByCustomID(inputer.InputOrderIDToUpdate(ordersService.GetOrdersByLogin(chosenlogin)), chosenlogin, inputer.InputOrderStatus());
                            Console.WriteLine();
                            break;
                        case "8":
                            current.CurrentRights = Rights.Guest;
                            break;
                    }

                }

            }
        }
        public void ShowMenu(CurrentUser current)
        {
            if (current.CurrentRights == Rights.Guest)
            {
                Console.WriteLine("Show product list - 0");
                Console.WriteLine("Search product by name - 1");
                Console.WriteLine("Registration - 2");
                Console.WriteLine("Enter to system - 3");
            }
            else if (current.CurrentRights == Rights.RegistredUser)
            {
                Console.WriteLine("Show product list - 0");
                Console.WriteLine("Search product by name - 1");
                Console.WriteLine("Create new order - 2");
                Console.WriteLine("Order cancellation - 3");
                Console.WriteLine("View order history and delivery status - 4");
                Console.WriteLine("Setting the status of the order 'Received' - 5");
                Console.WriteLine("Change personal information - 6");
                Console.WriteLine("Exit - 7");
            }
            else if (current.CurrentRights == Rights.Admin)
            {
                Console.WriteLine("Show product list - 0");
                Console.WriteLine("Search product by name - 1");
                Console.WriteLine("Create new order - 2");
                Console.WriteLine("Order cancellation - 3");
                Console.WriteLine("View and change personal information of users - 4");
                Console.WriteLine("Add new product - 5");
                Console.WriteLine("Change information about product - 6");
                Console.WriteLine("Change order status - 7");
                Console.WriteLine("Exit - 8");
            }

        }

    }
}
