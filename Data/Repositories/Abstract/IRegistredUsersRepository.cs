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
        RegistredUserEntity GetUserByLogin(string login);
        List<RegistredUserEntity> GetRegistredUsers();
    }
}
