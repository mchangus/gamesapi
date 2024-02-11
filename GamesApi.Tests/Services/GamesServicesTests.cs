using Games.Domain.Models;
using Games.Services;
using Games.Services.Abstracts;
using GamesApi.Rawg.Services.Abstracts;
using Moq;

namespace GamesApi.Tests.Services
{
    public class GamesServicesTests
    {
        private readonly Mock<IRawgApiService> _rawgApiServiceMock;
        private readonly IGamesService _sut;
        public GamesServicesTests()
        {
            _rawgApiServiceMock = new Mock<IRawgApiService>();

            _sut = new GamesService(_rawgApiServiceMock.Object);
        }

        [Fact]
        public async Task SearchAsync_Parameternull_throw_NullException()
        {
            // Arrange
            Search? search = null;

            // Act & Assert
            _ = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.SearchAsync(search));
        }

        [Fact]
        public async Task SearchAsync_ParameterValid_shouldCall_()
        {
            // Arrange
            Search search = new Search 
            {
                Query = "Mario",
                Sort = "name"
            };

            // Act 
            _ = await _sut.SearchAsync(search);

            // Assert
            _rawgApiServiceMock.Verify(x => x.GetGamesAsync(search.Query, search.Sort), Times.Once());
        }
    }
}