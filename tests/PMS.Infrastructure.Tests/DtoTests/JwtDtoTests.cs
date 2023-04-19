using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Tests.DtoTests
{
    public class JwtDtoTests
    {
        [Fact]
        public void JwtDtoConstructorSetsPropertiesCorrectly()
        {
            var token = "token12312@!(*@*";
            var expires = 8493408342984231L;


            var jwtDto = new JwtDto(token,expires);


            Assert.Equal(token, jwtDto.Token);
            Assert.Equal(expires, jwtDto.Expires);
        }
    }
}