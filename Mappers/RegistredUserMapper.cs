using Domain;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mappers
{
    public static class RegistredUserMapper
    {
        public static RegistredUserEntity ToEntity(this RegistredUser registredUser)
        {
            return new RegistredUserEntity()
            {
                Name = registredUser.Name,
                Lastname = registredUser.Lastname,
                Email = registredUser.Email,
                Login = registredUser.Login,
                Password = registredUser.Password,
                Rights = (int)registredUser.Status                
            };
        }
        public static RegistredUser ToDomain(this RegistredUserEntity registredUser)
        {
            if (registredUser == null) return null;
            return new RegistredUser
            {
                Name = registredUser.Name,
                Lastname = registredUser.Lastname,
                Email = registredUser.Email,
                Login = registredUser.Login,
                Password = registredUser.Password,
                Status = (Domain.Enums.UserStatus)registredUser.Rights
            };
        }
    }
}
