using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    class Inputer
    {
        public string InputName()
        {
            Console.WriteLine("Input name:");
            return Console.ReadLine();
        }

        public string InputSurname()
        {
            Console.WriteLine("Input surname:");
            return Console.ReadLine();
        }

        public string InputEmail()
        {
            Console.WriteLine("Input Email:");
            return Console.ReadLine();
        }

        public string InputLogin()
        {
            Console.WriteLine("Input login:");
            return Console.ReadLine();
        }

        public string InputPassword()
        {
            Console.WriteLine("Password:");
            return Console.ReadLine();
        }

        public string InputProductName()
        {
            Console.WriteLine("Input product name:");
            return Console.ReadLine();
        }
        public string InputProductCategory()
        {
            Console.WriteLine("Input product category:");
            return Console.ReadLine();
        }
        public string InputProductDescription()
        {
            Console.WriteLine("Input product description:");
            return Console.ReadLine();
        }
        public string InputProductCost()
        {
            Console.WriteLine("Input product cost:");
            return Console.ReadLine();
        }
        public string InputProductCode()
        {
            Console.WriteLine("Input product code:");
            return Console.ReadLine();
        }

        public List<string> FormBucket(List<Domain.Product> products)
        {
            Checker checker = new Checker();
            List<string> bucket = new List<string>();
            Console.WriteLine("If you want to leave creation menu, type - 'exit' ");
            while (true)
            {
                Console.WriteLine("Input product code:");
                string productCode = Console.ReadLine();
                if(productCode == "exit")
                {
                    return bucket;
                }
                if(checker.IsProductCodeExists(products, productCode) == true)
                {
                    bucket.Add(productCode);
                    Console.WriteLine("Finish creation - 0\nIf you want to order something else - press any button");
                    string choice = Console.ReadLine();
                    if (choice == "0")
                    {
                        return bucket;
                    }
                    continue;
                }
                Console.WriteLine("Product with such code doesn`t exist");
            }
        }
        public int InputOrderIDToUpdate(List<Domain.Order> orders)
        {
            Outputer outputer = new Outputer();
            Checker checker = new Checker();
            outputer.OutputUsersOrders(orders);
            string choice;
            while (true)
            {
                Console.WriteLine("Input order number to status update it");
                choice = Console.ReadLine();
                checker.CheckIsNotEmpty(choice);
                checker.IsFieldDigit(ref choice);
                if (checker.IsFieldInRange(Convert.ToInt32(choice), orders.Count))
                {
                    break;
                }
                Console.WriteLine("There is no order with such order number");

            }
            return Convert.ToInt32(choice) - 1;
        }
        public Domain.RegistredUser InputNewInfo(Domain.RegistredUser userToupdate)
        {
            Checker checker = new Checker();
            userToupdate.Name = checker.CheckIsNotEmpty(InputName());
            userToupdate.Lastname = checker.CheckIsNotEmpty(InputSurname());
            Console.Write("New ");
            userToupdate.Password = checker.CheckIsNotEmpty(InputPassword());
            userToupdate.Login = userToupdate.Login;
            userToupdate.Orders = userToupdate.Orders;
            userToupdate.Email = userToupdate.Email;
            userToupdate.Status = userToupdate.Status;
            return userToupdate;
        }

        public Domain.Product InputNewProductInfo(Domain.Product productToupdate)
        {
            Checker checker = new Checker();
            productToupdate.Name = checker.CheckIsNotEmpty(InputProductName());
            productToupdate.Category = checker.CheckIsNotEmpty(InputProductCategory());
            productToupdate.Description = checker.CheckIsNotEmpty(InputProductDescription());
            productToupdate.Cost = checker.IsFieldDecimal(InputProductCost());
            productToupdate.CodeProduct = checker.CheckIsNotEmpty(InputProductCode());
            return productToupdate;
        }

        public bool IsAdminWantToChange()
        {
            Console.WriteLine("If you want to change something, press - 0\nIf not, press any another symbol");
            string choice = Console.ReadLine();
            if (choice == "0")
            {
                return true;
            }
            return false;
        }
        public Domain.Enums.OrderStatus InputOrderStatus()
        {
            Checker checker = new Checker();
            while (true)
            {
                Console.WriteLine("Select order status: \nAdminDeny - 1 \nPayReceived - 2 \nSent - 3 \nCompleted - 4");
                string status = Console.ReadLine();
                checker.IsFieldDigit(ref status);
                if(checker.IsFieldInRange(Convert.ToInt32(status), 4))
                {
                    return (Domain.Enums.OrderStatus)Convert.ToInt32(status);
                }
            }
        }
    }
}
