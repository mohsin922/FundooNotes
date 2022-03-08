
namespace BusinessLayer.Interfaces
{
    using CommonLayer.Models;
    using RepositoryLayer.Entities;
    using System.Collections.Generic;

    public interface IUserBL
    {
        public User Registration(UserRegistrationModel userRegModel);
        public LoginResponseModel UserLogin(UserLoginModel userLog);

        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
        public IEnumerable<User> GetAllUsers();
    }
}