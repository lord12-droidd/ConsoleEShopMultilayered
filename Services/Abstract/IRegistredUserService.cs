using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IRegistredUserService
    {
        bool AddUser(RegistredUser registredUser);
        void UpdateUser(RegistredUser registredUser);
        RegistredUser GetUserByID(int id);
        RegistredUser GetUserByLogin(string login);
        RegistredUser DeleteUserByID(int id);
        List<RegistredUser> GetRegistredUsers();
    }
}
