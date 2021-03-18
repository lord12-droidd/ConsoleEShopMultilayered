using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Abstract
{
    public interface IRegistredUsersRepository
    {
        bool AddUser(RegistredUserEntity registredUser);
        void UpdateUser(RegistredUserEntity registredUser);
        RegistredUserEntity GetUserByID(int id);
        RegistredUserEntity GetUserByLogin(string login);
        RegistredUserEntity DeleteUserByID(int id);
        List<RegistredUserEntity> GetRegistredUsers();
    }
}
