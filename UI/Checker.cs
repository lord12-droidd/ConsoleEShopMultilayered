using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    class Checker
    {
        public bool CheckField(ref string field)
        {
            while (true)
            {
                if (field != "")
                {
                    return true;
                }
                Console.WriteLine("Field can`t be an empty");
                field = Console.ReadLine();
            }
        }
        public string CheckIsNotEmpty(string field)
        {
            while (true)
            {
                if (field != "")
                {
                    return field;
                }
                Console.WriteLine("Field can`t be an empty");
                field = Console.ReadLine();
            }
        }
        
        public bool IsProductCodeExists(List<Domain.Product> products, string productCode)
        {
            for(int i = 0; i < products.Count; i++)
            {
                if(products[i].CodeProduct == productCode)
                {
                    return true;
                }
            }
            return false;
        }

        public int IsFieldDigit(ref string field)
        {
            while (true)
            {
                try
                {
                    return Convert.ToInt32(field);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input a number please");
                    field = Console.ReadLine();
                }
                
            }
        }
        public decimal IsFieldDecimal(string field)
        {
            while (true)
            {
                try
                {
                    return Convert.ToDecimal(field);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input a number please");
                    field = Console.ReadLine();
                }

            }
        }
        public bool IsFieldInRange(int field, int maxRange)
        {
            if(field <= maxRange && field > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsUserExists(List<Domain.RegistredUser> registredUsers, string login)
        {
            for (int i = 0; i < registredUsers.Count; i++)
            {
                if (registredUsers[i].Login == login)
                {
                    return true;
                }
            }
            Console.WriteLine("User with such login doesn`t exist");
            return false;
        }

    }
}
