using Moq;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Services;
using System.Security.Authentication;
using System.Security.Cryptography;

namespace PMS.Infrastructure.UnitTests.Services
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserProfileRepository> _userProfileRepositoryMock;
        private Mock<IJwtService> _jwtServiceMock;
        private UserService _userService;


        private void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
            _jwtServiceMock = new Mock<IJwtService>();
            _userService = new UserService(_userRepositoryMock.Object, _userProfileRepositoryMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsyncShouldInvokeAddAsyncOnUserRepository()
        {
            Setup();

            await _userService.RegisterAsync("test@mail.com", "passW0!RDxO", "Test", "Test", "123456789");

            _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsyncShouldInvokeCreateTokenOnJwtServiceAndReturnTokenDto()
        {
            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var password = "pa$Sw0RD";
            var email = "test@mail.com";
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var user = new User(userId, userProfileId, email, passwordHash, passwordSalt);
            var token = new TokenDto(Guid.NewGuid().ToString(), DateTime.Now.AddDays(1).Ticks, user.Role);

            Setup();

            _userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);
            _jwtServiceMock.Setup(x => x.CreateToken(user)).Returns(token);


            var result = await _userService.LoginAsync(email, password);


            _jwtServiceMock.Verify(x => x.CreateToken(user), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(token.Token, result.Token);
            Assert.Equal(token.Expires, result.Expires);
            Assert.Equal(token.Role, result.Role);
        }

        [Fact]
        public async Task LoginAsyncShouldThrowInvalidCredentialsExceptionWhenUserNotFound()
        {
            Setup();

            var email = "test@mail.com";
            var password = "pa$SW0rDD!X";

            _userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<InvalidCredentialException>(() => _userService.LoginAsync(email, password));
        }

        [Fact]
        public async Task LoginAsyncShouldThrowInvalidCredentialsExceptionPasswordIncorrect()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var password = "pa$Sw0RD";
            var email = "test@mail.com";
            byte[] passwordHash;
            byte[] passwordSalt;

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            var user = new User(userId, userProfileId, email, passwordHash, passwordSalt);

            _userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            await Assert.ThrowsAsync<InvalidCredentialException>(() => _userService.LoginAsync(user.Email, "incorrectPassword"));      
        }

        [Fact]
        public async Task GetAsyncShouldReturnUserDtoIfIdIsCorrect()
        {
            Setup();

            var userId = Guid.NewGuid();
            var userProfileId = Guid.NewGuid();
            var email = "test@mail.com";
            byte[] passwordHash = new byte[64];
            byte[] passwordSalt = new byte[128];

            var user = new User(userId,userProfileId, email, passwordHash, passwordSalt);

            _userRepositoryMock.Setup(x => x.GetAsync(userId)).ReturnsAsync(user);

            var result = await _userService.GetAsync(user.Id);

            _userRepositoryMock.Verify(x => x.GetAsync(user.Id), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(email, result.Email);
            Assert.Equal(userProfileId, result.UserProfileId);
        }

        [Fact]
        public async Task GetAsyncThrowsUserNotFoundExceptionWhenUserWithIdDoesNotExist()
        {
            Setup();

            var id = Guid.NewGuid();

            await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetAsync(id));
        }
    }
}