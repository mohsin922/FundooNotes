namespace RepositoryLayer.Interface
{
    using CommonLayer.Models;
    using RepositoryLayer.Entities;
    using System.Collections.Generic;
    public interface IUserRL
    {
        /// <summary>
        /// Interface for User Registration
        /// </summary>
        /// <param name="userRegModel"></param>
        /// <returns></returns>
        public User Registration(UserRegistrationModel userRegModel);

        //public string Login(UserLoginModel userLogin);

        /// <summary>
        /// Interface for UserLogin
        /// </summary>
        /// <param name="userLog"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginModel userLog);

        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
        public IEnumerable<User> GetAllUsers();

    }
}