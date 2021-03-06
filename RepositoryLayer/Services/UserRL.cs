namespace RepositoryLayer.Services
{
    using CommonLayer.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entities;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        IConfiguration _Appsettings;
        public UserRL(FundooContext fundooContext, IConfiguration Appsettings)
        {
            this.fundooContext = fundooContext;
            _Appsettings = Appsettings;
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="userRegModel"></param>
        /// <returns></returns>
        public User Registration(UserRegistrationModel userRegModel)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegModel.FirstName;
                newUser.LastName = userRegModel.LastName;
                newUser.Email = userRegModel.Email;
                newUser.Password = EncryptPassword(userRegModel.Password);
                fundooContext.UserTables.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return newUser;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Method for EncryptPassword
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncryptPassword(string password)
        {
            string PasswordEntered = "";
            byte[] hide = new byte[password.Length];
            hide = Encoding.UTF8.GetBytes(password);
            PasswordEntered = Convert.ToBase64String(hide);
            return PasswordEntered;
        }

        private string Decrypt(string encryptpass)
        {
            string decrypt = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpass);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decrypt = new String(decoded_char);
            return decrypt;
        }
        /// <summary>
        /// Show All Registerd Login Data
        /// </summary>
        /// <param name="userLog"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginModel userLog)
        {
            try
            {
                var existingLogin = this.fundooContext.UserTables.Where(X => X.Email == userLog.Email).FirstOrDefault();
                if (Decrypt(existingLogin.Password) == userLog.Password)
                {
                    LoginResponseModel login = new LoginResponseModel();
                    string token = GenerateSecurityToken(existingLogin.Email, existingLogin.Id);
                    login.Id = existingLogin.Id;
                    login.FirstName = existingLogin.FirstName;
                    login.LastName = existingLogin.LastName;
                    login.Email = existingLogin.Email;
                    login.Password = existingLogin.Password;
                    login.CreatedAt = existingLogin.CreatedAt;
                    login.Token = token;

                    return login;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generating Security Token
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private string GenerateSecurityToken(string Email, long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var existingLogin = this.fundooContext.UserTables.Where(X => X.Email == email).FirstOrDefault();
                if (existingLogin != null)
                {
                    var token = GenerateSecurityToken(email, existingLogin.Id);
                    new MSMQ_Model().MSMQSender(token);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    User user = fundooContext.UserTables.Where(e => e.Email == email).FirstOrDefault();
                    user.Password = confirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                var result = this.fundooContext.UserTables.ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}