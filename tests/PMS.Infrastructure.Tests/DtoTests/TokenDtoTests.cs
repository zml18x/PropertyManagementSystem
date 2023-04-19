using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Tests.DtoTests
{
    public class TokenDtoTests
    {
        [Fact]
        public void JwtDtoConstructorSetsPropertiesCorrectly()
        {
            var token = "token12312@!(*@*";
            var expires = 8493408342984231L;
            var role = "User";


            var jwtDto = new TokenDto(token, expires, role);


            Assert.Equal(token, jwtDto.Token);
            Assert.Equal(expires, jwtDto.Expires);
            Assert.Equal(role, jwtDto.Role);
        }
    }
}