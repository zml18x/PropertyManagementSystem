﻿using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Account
{
    public class Register
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,MinLength(8),MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required,Phone]
        public string PhoneNumber { get; set; }

        public Register(string email, string password, string firstName, string lastName, string phoneNumber)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}