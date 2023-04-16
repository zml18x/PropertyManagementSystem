using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Infrastructure.Requests.Account
{
#nullable disable
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
    }
#nullable enable
}