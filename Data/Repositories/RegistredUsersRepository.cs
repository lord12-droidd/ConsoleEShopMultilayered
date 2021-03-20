using Data.Repositories.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class RegistredUsersRepository : IRegistredUsersRepository
    {
        private static List<RegistredUserEntity> _registredUsers = new List<RegistredUserEntity>() 
        {
            new RegistredUserEntity(){ID = 0, Name = "Dima",Lastname = "Melnyk",Email = "sfsdf@.com",Login = "Admin",Password = "Admin", Rights = 2}
        };

        public bool AddUser(RegistredUserEntity registredUser)
        {
            for(int i = 0; i < _registredUsers.Count; i++)
            {
                if(_registredUsers[i].Email == registredUser.Email || _registredUsers[i].Login == registredUser.Login)
                {
                    return false;
                }
            }
            registredUser.ID = _registredUsers.Count + 1;
            registredUser.Rights = 1;
            _registredUsers.Add(registredUser);
            return true;
        }


        public RegistredUserEntity GetUserByLogin(string login)
        {
            for(int i = 0; i < _registredUsers.Count; i++)
            {
                if(_registredUsers[i].Login == login)
                {
                    return _registredUsers[i];
                }
            }
            return new RegistredUserEntity();
        }

        public void UpdateUser(RegistredUserEntity registredUser)
        {
            for(int i = 0; i < _registredUsers.Count; i++)
            {
                if(registredUser.Login == _registredUsers[i].Login)
                {
                    _registredUsers[i].Name = registredUser.Name;
                    _registredUsers[i].Lastname = registredUser.Lastname;
                    _registredUsers[i].Password = registredUser.Password;
                }
            }
        }
        public List<RegistredUserEntity> GetRegistredUsers()
        {
            List<RegistredUserEntity> registredUserEntities = new List<RegistredUserEntity>();
            for (int i = 0; i < _registredUsers.Count; i++)
            {
                registredUserEntities.Add(_registredUsers[i]);
            }
            return registredUserEntities;
        }
    }
}
