﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.WEB.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }        
        [Required]
        public string Role { get; set; }

        public RegisterModel()
        {
            Role = "Customer";
        }
    }
}