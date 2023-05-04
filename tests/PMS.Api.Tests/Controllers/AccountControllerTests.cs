using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PMS.Api.Controllers;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Interfaces;
using PMS.Infrastructure.Requests.Account;
using System.Security.Claims;

namespace PMS.Api.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IUserService> _userService;



        public AccountControllerTests()
        {
            _userService = new Mock<IUserService>();
        }



        [Fact]
        public async Task RegisterAsync_ShouldInvokeRegisterAsyncOnUserServiceAndRetrun201StatusCode()
        {
            var email = "test@mail.com";
            var password = "pa$SW0rD!";
            var firstName = "Test";
            var lastName = "TestTest";
            var phoneNumber = "123456789";

            var accountController = new AccountController(_userService.Object);
            var request = new Register(email, password, firstName, lastName, phoneNumber);

            var result = await accountController.RegisterAsync(request);

            _userService.Verify(x => x.RegisterAsync(email, password, firstName, lastName, phoneNumber), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);

            var createdResult = (CreatedResult)result;
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal("/Account", createdResult.Location);
        }

        [Fact]
        public async Task LoginAsync_ShouldInvokeLoginAsyncOnUserServiceAndReturnTokenDtoInJson()
        {
            var email = "test@mail.com";
            var password = "pa$SW0rD!";
            var tokenDto = new TokenDto("token", DateTime.Now.AddDays(1).Ticks, "User");

            var accountController = new AccountController(_userService.Object);
            var request = new Login(email, password);

            _userService.Setup(x => x.LoginAsync(email, password)).ReturnsAsync(tokenDto);

            var result = await accountController.LoginAsync(request);
            var jsonResult = (JsonResult)result;


            _userService.Verify(x => x.LoginAsync(email, password), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(tokenDto, jsonResult.Value);
        }

        [Fact]
        public async Task GetAsync_ShouldInvokeGetAsyncOnUserServiceAndReturnAccountDtoInJson()
        {
            var userId = Guid.NewGuid();
            var accountController = new AccountController(_userService.Object);

            accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            var userDto = new UserDto(userId, Guid.NewGuid(), "User", "test@mail.com");
            _userService.Setup(x => x.GetAsync(userId)).ReturnsAsync(userDto);

            var result = await accountController.GetAsync();
            var jsonResult = (JsonResult)result;

            _userService.Verify(x => x.GetAsync(userId), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(userDto, jsonResult.Value);
        }
    }
}