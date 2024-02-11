using AutoFixture;
using Games.Api.Controllers;
using Games.Core.Models;
using Games.Domain.Models;
using Games.Services;
using Games.Services.Abstracts;
using GamesApi.Domain.Models.Requests;
using GamesApi.Domain.Models.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GamesApi.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;

        private readonly UsersController _sut;

        public UsersControllerTests() 
        {
            _usersServiceMock = new Mock<IUsersService>();
            _loggerMock = new Mock<ILogger<UsersController>>();

            _sut = new UsersController(_loggerMock.Object,
                _usersServiceMock.Object);

        }

        [Fact]
        public void GetUserById_ShouldReturn_NotFound_if_user_not_found() 
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();

            User? user = null;

            _usersServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            // Act
            var actual = _sut.GetUserById(userId);

            // Assert

            var notFoundResult = actual as NotFoundResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetUserById_ShouldReturn_user_and_200()
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();

            User user = fix.Create<User>();

            _usersServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            // Act
            var actual = _sut.GetUserById(userId);

            // Assert

            var okResult = actual as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void Create_ShouldCallService_and_Return_user_with_code_created()
        {
            // Arrange


            // Act
            var actual = _sut.CreateUser();

            // Assert
            _usersServiceMock.Verify(x => x.Create(), Times.Once);
            var createdResult = actual as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }


        [Fact]
        public async Task AddGameToFavorite_ShouldCallService_and_return_no_content_if_succeded()
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            AddFavoriteRequestBody body = fix.Create<AddFavoriteRequestBody>();
            ResultWithData<User> resultWithData = ResultWithData<User>.Success(new User());

            _usersServiceMock.Setup(x => x.AddGameToFavoriteAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(resultWithData);

            // Act
            var actual = await _sut.AddGameToFavorite(userId, body);

            // Assert
            _usersServiceMock.Verify(x => x.AddGameToFavoriteAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            var noContentResult = actual as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task AddGameToFavorite_ShouldCallService_and_return_specific_error_code400_if_not_succeded()
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();

            AddFavoriteRequestBody body = fix.Create<AddFavoriteRequestBody>();
            ResultWithData<User> resultWithData = ResultWithData<User>.Failure("", StatusCodes.Status400BadRequest);

            _usersServiceMock.Setup(x => x.AddGameToFavoriteAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(resultWithData);

            // Act
            var actual = await _sut.AddGameToFavorite(userId, body);

            // Assert
            _usersServiceMock.Verify(x => x.AddGameToFavoriteAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

            var result = actual as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public void RemoveGameFromFavorite_ShouldCallService_and_return_no_content_if_succeded()
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            AddFavoriteRequestBody body = fix.Create<AddFavoriteRequestBody>();
            ResultWithData<User> resultWithData = ResultWithData<User>.Success(new User());

            _usersServiceMock.Setup(x => x.RemoveGameFromFavorite(It.IsAny<int>(), It.IsAny<int>())).Returns(resultWithData);

            // Act
            var actual = _sut.RemoveGameFromFavorite(userId, gameId);

            // Assert
            _usersServiceMock.Verify(x => x.RemoveGameFromFavorite(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            var noContentResult = actual as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public void CompareFavorite_ShouldCallService_and_return_comparison_object__with_200_if_succeded()
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            ComparisonRequest request = fix.Create<ComparisonRequest>();

            ResultWithData<ComparisonResponse> resultWithData = ResultWithData<ComparisonResponse>.Success(new ComparisonResponse());

            _usersServiceMock.Setup(x => x.CompareFavorite(It.IsAny<int>(), It.IsAny<ComparisonRequest>())).Returns(resultWithData);

            // Act
            var actual = _sut.CompareFavorite(userId, request);

            // Assert
            _usersServiceMock.Verify(x => x.CompareFavorite(It.IsAny<int>(), It.IsAny<ComparisonRequest>()), Times.Once);
            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }
    }
}
