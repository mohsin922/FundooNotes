using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public User Registration(UserRegistrationModel userRegModel);
        public LoginResponseModel UserLogin(UserLoginModel userLog);
    }
}