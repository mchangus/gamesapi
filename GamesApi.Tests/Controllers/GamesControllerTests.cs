using Games.Api.Controllers;
using Games.Domain.Models;
using Games.Domain.Models.Configuration;
using Games.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace GamesApi.Tests.Controllers
{
    public class GamesControllerTests
    {
        private readonly Mock<IGamesService> _gamesServiceMock;
        private readonly Mock<ILogger<GamesController>> _loggerMock;
        private readonly Mock<IOptions<RAWGSettings>> _searchSettingsMock;

        private GamesController _sut;
        public GamesControllerTests() 
        {
            _gamesServiceMock = new Mock<IGamesService>();
            _loggerMock = new Mock<ILogger<GamesController>>();
            _searchSettingsMock = new Mock<IOptions<RAWGSettings>>();

            RAWGSettings settings = new RAWGSettings {
                GameSearchOrderingOptions = new List<string> { "name", "-name" }
            };

            _searchSettingsMock.SetupGet(x => x.Value).Returns(settings);

            _sut = new GamesController(_loggerMock.Object,
                _searchSettingsMock.Object,
                _gamesServiceMock.Object);
        }

        [Fact]
        public async Task GetGames_ValidationFailed_should_return_BadRequest()
        {
            // Arrange
            Search search = new Search
            {
                Query = "Mario",
                Sort = "rating"
            };

            // Act

            var response = await _sut.GetGames(search);

            var badRequestResult = response as BadRequestObjectResult;
            // Assert

            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

        }

        [Fact]
        public async Task GetGames_Search_ValidationPassed_should_CallService_and_return_GamesCollection()
        {
            // Arrange

            RAWGSettings settings = new RAWGSettings
            {
                GameSearchOrderingOptions = new List<string> { "rating", "-rating" }
            };

            _searchSettingsMock.SetupGet(x => x.Value).Returns(settings);

            _sut = new GamesController(_loggerMock.Object,
                _searchSettingsMock.Object,
                _gamesServiceMock.Object);

            Search search = new Search
            {
                Query = "Mario",
                Sort = "rating"
            };

            // Act

            var response = await _sut.GetGames(search);

            var okResult = response as OkObjectResult;
            // Assert

            _gamesServiceMock.Verify(x => x.SearchAsync(It.IsAny<Search>()), Times.Once);
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        }
    }
}
