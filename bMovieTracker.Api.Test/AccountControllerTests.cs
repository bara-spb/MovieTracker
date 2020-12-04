using bMovieTracker.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using bMovieTracker.Api.Test.Helpers;
using bMovieTracker.Identity;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Identity;

namespace bMovieTracker.Api.Test
{
    public class AccountControllerTests
    {

        #region Setup Dependencies

        private static Mock<MovieTrackerUserManager> GetMockUserManager()
        {
            var mockIUserStore = new Mock<IUserStore<MovieTrackerUser>>();
            var mockUserManger = new Mock<MovieTrackerUserManager>(mockIUserStore.Object, null, null, null, null, null, null, null, null);
            mockUserManger.Setup(um => um.FindByNameAsync(It.IsAny<string>()))
                .Returns<string>(name => FindByNameAsync(name));
            mockUserManger.Setup(um => um.CheckPasswordAsync(It.IsAny<MovieTrackerUser>(), It.IsAny<string>()))
                .Returns<MovieTrackerUser, string>((user, pwd) => CheckPasswordAsync(user, pwd));
            mockUserManger.Setup(um => um.CreateAsync(It.IsAny<MovieTrackerUser>(), It.IsAny<string>()))
                .Returns<MovieTrackerUser, string>((user, pwd) => CreateUserAsync(user, pwd));

            
            return mockUserManger;
        }

        private static Dictionary<MovieTrackerUser, string> userPasswordList = new Dictionary<MovieTrackerUser, string>()
            {
                {  new MovieTrackerUser() { UserName = "user1" }, "password1" },
                {  new MovieTrackerUser() { UserName = "user2" }, "password2" },
                {  new MovieTrackerUser() { UserName = "user3" }, "password3" }
            };

        private static async Task<MovieTrackerUser> FindByNameAsync(string name)
        {
            var user = userPasswordList.Where(up => up.Key.UserName == name).Select(up => up.Key).FirstOrDefault();
            return user;
        }

        private static async Task<IdentityResult> CreateUserAsync(MovieTrackerUser user, string name)
        {
            if (user.UserName == "newUserValid")
                return IdentityResult.Success;
            else
                return IdentityResult.Failed();
        }

        private static async Task<bool> CheckPasswordAsync(MovieTrackerUser user, string password)
        {
            var correctPassword = userPasswordList.Where(up => up.Key == user).Select(up => up.Value).FirstOrDefault();
            return !string.IsNullOrEmpty(password) && password.Equals(correctPassword);
        }

        #endregion

        public class LoginTests
        {

            [Fact]
            public async Task LoginEmptyValuesTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Login("", "") as ObjectResult;

                // Assert            
                Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
                Assert.Equal("Please, specify username and password", result.Value);
            }

            [Fact]
            public async Task LoginWrongCredentialsTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Login("qwerty", "qwerty") as ObjectResult;

                // Assert            
                Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
                Assert.Equal("No account with this username/password combination was found", result.Value);
            }

            [Fact]
            public async Task LoginSuccessTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Login("user1", "password1") as ObjectResult;                

                // Assert   
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
                Assert.True(result.Value.HasProperty("access_token"));
            }

        }

        public class RegisterTests
        {            

            [Fact]
            public async Task RegisterEmptyValuesTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Register("", "") as ObjectResult;

                // Assert            
                Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
                Assert.Equal("Please, specify email and password", result.Value);
            }

            [Fact]
            public async Task RegisterDuplicateEmailTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Register("user1", "qwerty") as ObjectResult;

                // Assert            
                Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
                Assert.Equal("This Email is already registered", result.Value.GetPropValue("message"));
            }

            [Fact]
            public async Task RegisterSuccessTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Register("newUserValid", "password") as ObjectResult;

                // Assert   
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
                Assert.True(result.Value.HasProperty("access_token"));
            }

            [Fact]
            public async Task RegisterInternalErrorTest()
            {
                // Arrange
                var userManager = GetMockUserManager().Object;
                var controller = new AccountController(userManager);

                // Act
                var result = await controller.Register("newUserInvalid", "password") as ObjectResult;

                // Assert   
                Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            }
        }

    }
}
