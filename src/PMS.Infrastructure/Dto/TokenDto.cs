using System.Data;

namespace PMS.Infrastructure.Dto
{
    public class TokenDto : JwtDto
    {
        public string Role { get; set; }

        public TokenDto(string token, long expires, string role) : base (token,expires)
        {
            Role = role;
        }
    }
}
