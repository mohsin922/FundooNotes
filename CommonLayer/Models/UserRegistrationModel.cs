namespace CommonLayer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Model class for registering all the details required to (sign up)
    /// </summary>
    public class UserRegistrationModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Z a-z]{3,}[!*@#$%^&+=]?[0-9]{1,}$")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}