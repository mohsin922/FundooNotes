﻿using System;

namespace CommonLayer.Models
{
    /// <summary>
    /// Model class for getting Email and password
    /// </summary>
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}