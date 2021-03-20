using Data.Repositories.Abstract;
using Domain;
using Mappers;
using Services.Abstract;
using System;
using Data;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class RegistredUserService : IRegistredUserService
    {
        private readonly IRegistredUsersRepository _registredUserRepository;

        public RegistredUserService(IRegistredUsersRepository registredUsersRepository)
        {
            _registredUserRepository = registredUsersRepository;
        }

        Data.Repositories.RegistredUsersRepository registredUsersRepository = new Data.Repositories.RegistredUsersRepository();
        public bool AddUser(RegistredUser registredUser)
        {
            if(registredUsersRepository.AddUser(registredUser.ToEntity()) == true)
            {
                return true;
            }
            return false;
        }

        public RegistredUser GetUserByLogin(string login)
        {
            return registredUsersRepository.GetUserByLogin(login).ToDomain();
        }

        public void UpdateUser(RegistredUser registredUser)
        {
            registredUsersRepository.UpdateUser(registredUser.ToEntity());
        }
        public List<RegistredUser> GetRegistredUsers()
        {
            List<RegistredUser> registredUsers = new List<RegistredUser>();
            for (int i = 0; i < registredUsersRepository.GetRegistredUsers().Count; i++)
            {
                registredUsers.Add(RegistredUserMapper.ToDomain(registredUsersRepository.GetRegistredUsers()[i]));
            }
            return registredUsers;
        }
    }
}
