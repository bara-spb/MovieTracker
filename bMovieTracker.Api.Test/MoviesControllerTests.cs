using bMovieTracker.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using bMovieTracker.Identity;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Identity;
using bMovieTracker.App;
using bMovieTracker.Domain;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace bMovieTracker.Api.Test
{
    public class MoviesControllerTests
    {

        #region Setup Dependencies

        IEnumerable<MovieVM> movies = new List<MovieVM>()
        {
            new MovieVM() {
                Id = 1,
                Title = "Movie1",
                Genre = GenreTypes.Action,
                Year = new ReleaseYear(1995),
                Rate = RateTypes.ThreeStars,
            },
            new MovieVM() {
                Id = 2,
                Title = "Movie2",
                Genre = GenreTypes.War,
                Year = new ReleaseYear(2000),
                Rate = RateTypes.FiveStars
            },
            new MovieVM() {
                Id = 3,
                Title = "Movie3",
                Genre = GenreTypes.Romance,
                Year = new ReleaseYear(2005),
                Rate = RateTypes.FourStars
            }
        };

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
            mockUserManger.Setup(um => um.GetUserIdInt(It.IsAny<ClaimsPrincipal>()))
                .Returns<int?>(p => 1);


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


        [Fact]
        public async Task GetAllMovieTest()
        {
            // Arrange
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.GetAllMovies(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, CancellationToken>((userId, ct) => Task.FromResult(movies));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var result = await controller.Get() as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<List<MovieVM>>(result.Value);
            Assert.Equal(3, ((IEnumerable<MovieVM>)result.Value).Count());
        }


        [Fact]
        public async Task GetMovieByIdTest()
        {
            // Arrange
            var movie = new MovieVM()
            {
                Id = 1,
                Title = "Movie1",
                Genre = GenreTypes.Action,
                Year = new ReleaseYear(1995),
                Rate = RateTypes.ThreeStars,
            };
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.GetMovieById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, int, CancellationToken > ((uId, id, ct) => Task.FromResult(movie));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var result = await controller.Get(1) as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<MovieVM>(result.Value);
            Assert.Equal("Movie1", ((MovieVM)result.Value).Title);
        }

        [Fact]
        public async Task CreateMovieTest()
        {
            // Arrange
            var movie = new MovieVM()
            {
                Id = 1,
                Title = "Movie1",
                Genre = GenreTypes.Action,
                Year = new ReleaseYear(1995),
                Rate = RateTypes.ThreeStars,
            };
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.CreateMovie(It.IsAny<int>(), It.IsAny<CreateMovieVM>(), It.IsAny<CancellationToken>()))
                .Returns<int, CreateMovieVM, CancellationToken>((uId, m, ct) => Task.FromResult(movie));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var result = await controller.Post(new CreateMovieVM()) as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.IsType<MovieVM>(result.Value);
            Assert.Equal("Movie1", ((MovieVM)result.Value).Title);
        }

        [Fact]
        public async Task CreateMovieEmptyModelTest()
        {
            // Arrange
            var movie = new MovieVM()
            {
                Id = 1,
                Title = "Movie1",
                Genre = GenreTypes.Action,
                Year = new ReleaseYear(1995),
                Rate = RateTypes.ThreeStars,
            };
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.CreateMovie(It.IsAny<int>(), It.IsAny<CreateMovieVM>(), It.IsAny<CancellationToken>()))
                .Returns<int, CreateMovieVM, CancellationToken>((uId, m, ct) => Task.FromResult(movie));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var result = await controller.Post(null) as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal("Model can't be empty", result.Value);
        }

        [Fact]
        public async Task UpdateMovieTest()
        {
            // Arrange
            var originMovie = new MovieVM()
            {
                Id = 1,
                Title = "Movie1",
                Genre = GenreTypes.Action,
                Year = new ReleaseYear(1995),
                Rate = RateTypes.ThreeStars,
            };
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.GetMovieById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, int, CancellationToken>((uId, id, ct) => Task.FromResult(originMovie));
            mockMoviesService.Setup(s => s.UpdateMovie(It.IsAny<int>(), It.IsAny<MovieVM>(), It.IsAny<CancellationToken>()))
                .Returns<int, MovieVM, CancellationToken>((uId, m, ct) => Task.FromResult(true));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var updatedMovie = new MovieVM()
            {
                Id = 1,
                Title = "Movie1_updated",
                Genre = GenreTypes.Action,
                Year = new ReleaseYear(1995),
                Rate = RateTypes.ThreeStars,
            };
            var result = await controller.Put(1, updatedMovie) as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<MovieVM>(result.Value);
            Assert.Equal("Movie1_updated", ((MovieVM)result.Value).Title);
        }

        [Fact]
        public async Task DeleteMovieTest()
        {
            // Arrange
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.GetMovieById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, int, CancellationToken>((uId, id, ct) => Task.FromResult(new MovieVM()));
            mockMoviesService.Setup(s => s.DeleteMovie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, int, CancellationToken>((uId, mId, ct) => Task.FromResult(true));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var result = await controller.Delete(1) as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Movie deleted", result.Value);
        }

        [Fact]
        public async Task DeleteNotExistedMovieTest()
        {
            // Arrange
            var userManager = GetMockUserManager().Object;
            var mockMoviesService = new Mock<IMovieTrackerService>();
            mockMoviesService.Setup(s => s.DeleteMovie(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns<int, int, CancellationToken>((uId, mId, ct) => Task.FromResult(false));
            var controller = new MoviesController(mockMoviesService.Object, userManager);

            // Act
            var result = await controller.Delete(1) as ObjectResult;

            // Assert            
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Movie is not found", result.Value);
        }

    }
}
