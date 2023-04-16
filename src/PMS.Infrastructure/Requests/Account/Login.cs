using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Account
{
    public class Login
    {
        [Required,EmailAddress,MinLength(5)]
        public string Email { get; set; }
        [Required,MinLength(8),MaxLength(100)]
        public string Password { get; set; }


        public Login(string email,string password)
        {
            Email = email;
            Password = password;
        }
    }
}