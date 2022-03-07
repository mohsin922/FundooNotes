namespace Microsoft.Xxx
{
    using BusinessLayer.Interfaces;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    namespace FundooNotes.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly IUserBL userBL;
            public UserController(IUserBL userBL)
            {
                this.userBL = userBL;
            }
            [HttpPost("Register")]
            public IActionResult addUser(UserRegistrationModel userRegModel)
            {
                try
                {
                    var result = userBL.Registration(userRegModel);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, message = "Registration Successful", data = result });
                    }
                    else
                        return this.BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
                catch (Exception)
                {
                    throw;
                }
            }


            /// <summary>
            /// Get all Login Data
            /// </summary>
            /// <param name="userLog"></param>
            /// <returns></returns>
            [HttpPost("Login")]
            public IActionResult UserLogin(UserLoginModel userLog)
            {
                try
                {
                    var result = userBL.UserLogin(userLog);
                    if (result != null)
                    {
                        return this.Ok(new { isSuccess = true, message = "Login Successfull!", data = result });
                    }
                    else
                        return this.BadRequest(new { isSuccess = false, message = "Login Unsuccessfull!" });
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            [HttpPost("ForgotPassword")]
            public IActionResult ForgotPassword(string email)
            {
                try
                {
                    var result = userBL.ForgetPassword(email);
                    if (result != null)
                    {
                        return this.Ok(new { isSuccess = true, message = "Forgot Password Link sent Successfully!" });
                    }
                    else
                        return this.BadRequest(new { isSuccess = false, message = "Email Is Incorrect.Try Again!" });
                }
                catch (Exception e)
                {

                    return this.BadRequest(new { isSuccess = false, message = e.InnerException.Message });
                }
            }

            [Authorize]
            [HttpPost("ResetPassword")]

            public IActionResult ResetPassword(string password, string confirmPassword)
            {
                try
                {
                    //var email = User.Claims.First(e => e.Type == "Email").Value;
                    var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                    var result = userBL.ResetPassword(email, password, confirmPassword);
                    return this.Ok(new { isSuccess = true, message = "Password Resetted Successfully!" });

                }
                catch (Exception e)
                {

                    return this.BadRequest(new { isSuccess = false, message = e.InnerException.Message });
                }
            }
        }
    }
}