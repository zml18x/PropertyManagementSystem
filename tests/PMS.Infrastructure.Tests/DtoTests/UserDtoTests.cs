using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Tests.DtoTests
{
    public class UserDtoTests
    {
        [Fact]
        public void UserDtoConstructorSetsPropertiesCorrectly()
        {
            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var role = "User";
            var email = "example@mail.com";


            var userDto = new UserDto(userId, userProfileId, role, email);

            Assert.Equal(userId, userDto.UserId);
            Assert.Equal(userProfileId, userDto.UserProfileId);
            Assert.Equal(role, userDto.Role);
            Assert.Equal(email, userDto.Email);
        }
    }
}