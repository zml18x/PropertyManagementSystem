using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Account
{
#nullable disable
    public class Login
    {
        [Required,EmailAddress,MinLength(5)]
        public string Email { get; set; }
        [Required,MinLength(8),MaxLength(100)]
        public string Password { get; set; }
    }
#nullable enable
}