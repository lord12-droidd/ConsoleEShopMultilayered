using Mappers;
using Models;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Controllers
{
    public class RegistredUserController
    {
        private readonly IRegistredUserService _registredUserService;
        public RegistredUserController(IRegistredUserService registredUserService)
        {
            _registredUserService = registredUserService;
        }
        public void AddRegistredUser(RegistredUserModel userModel)
        {
            if(userModel == null)
            {
                Console.WriteLine("There is no User info");
                return;
            }
            _registredUserService.AddUser(userModel.ToDomain());
        }
    }
}

