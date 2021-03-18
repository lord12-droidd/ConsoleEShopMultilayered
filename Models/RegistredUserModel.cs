using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RegistredUserModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
