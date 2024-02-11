using AutoFixture;
using Games.Core.Models;
using Games.Domain.Models;
using Games.Services;
using Games.Services.Abstracts;
using GamesApi.Domain.Models.Results;
using GamesApi.Rawg.Services.Abstracts;
using GamesApi.UsersRepository.Abstracts;
using Microsoft.AspNetCore.Http;
using Moq;

namespace GamesApi.Tests.Services
{
    public class UsersServicesTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;

        private readonly Mock<IRawgApiService> _rwgApiServiceMock;

        private IUsersService _sut;
        public UsersServicesTests()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _rwgApiServiceMock = new Mock<IRawgApiService>();

            _sut = new UsersService(_usersRepositoryMock.Object,
                _rwgApiServiceMock.Object);
        }

        [Fact]
        public void Create_ShouldCallRepo_and_Return_Created_User()
        {
            // Arrange

            Fixture fix = new Fixture();
            User expected = fix.Create<User>();
            _usersRepositoryMock.Setup(x => x.Create()).Returns(expected);

            // Act
            var actual = _sut.Create();

            // Assert
            _usersRepositoryMock.Verify(x => x.Create(), Times.Once);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetById_ShouldCallRepo_and_Return_User()
        {
            // Arrange

            Fixture fix = new Fixture();
            User expected = fix.Create<User>();
            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(expected);

            // Act
            var actual = _sut.GetById(expected.UserId);

            // Assert
            _usersRepositoryMock.Verify(x => x.GetById(expected.UserId), Times.Once);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddGameToFavoriteAsync_ShouldFailWith404_when_user_not_found()
        {
            // Arrange

            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = null;

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status404NotFound);

            // Act
            var actual = await _sut.AddGameToFavoriteAsync(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public async Task AddGameToFavoriteAsync_ShouldFailWith409_when_Game_is_already_in_favorites()
        {
            // Arrange

            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = fix.Build<User>()
                .With(x => x.UserId, userId)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId } })
                .Create();

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status409Conflict);

            // Act
            var actual = await _sut.AddGameToFavoriteAsync(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public async Task AddGameToFavoriteAsync_ShouldFailWith400_when_Game_does_not_exists()
        {
            // Arrange

            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = fix.Create<User>();

            Game? game = null;

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);
            _rwgApiServiceMock.Setup(x => x.GetGameByIdAsync(It.IsAny<int>())).ReturnsAsync(game);


            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status400BadRequest);



            // Act
            var actual = await _sut.AddGameToFavoriteAsync(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public async Task AddGameToFavoriteAsync_ShouldCallRepo_and_return_result_object()
        {
            // Arrange

            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = fix.Create<User>();

            Game game = fix.Create<Game>();

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);
            _rwgApiServiceMock.Setup(x => x.GetGameByIdAsync(It.IsAny<int>())).ReturnsAsync(game);


            ResultWithData<User> expected = ResultWithData<User>.Success(user);



            // Act
            var actual = await _sut.AddGameToFavoriteAsync(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
            Assert.Equal(expected.GetDataOnSuccess(), actual.GetDataOnSuccess());
        }

        [Fact]
        public void RemoveGameFromFavorite_ShouldFailWith400_when_user_not_found()
        {
            // Arrange

            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = null;

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status400BadRequest);

            // Act
            var actual = _sut.RemoveGameFromFavorite(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public void RemoveGameFromFavorite_ShouldFailWith404_when_game_not_found()
        {
            // Arrange

            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = fix.Create<User>();
            Game? game = null;

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status404NotFound);

            // Act
            var actual = _sut.RemoveGameFromFavorite(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public void RemoveGameFromFavorite_ShouldCallRepo_and_return_result_object()
        {
            // Arrange
            Fixture fix = new Fixture();
            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = fix.Build<User>()
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId } })
                .Create();

            Game game = fix.Build<Game>()
                .With(x => x.GameId, user.Games.First().GameId)
                .Create();

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);
            _rwgApiServiceMock.Setup(x => x.GetGameByIdAsync(It.IsAny<int>())).ReturnsAsync(game);
            ResultWithData<User> expected = ResultWithData<User>.Success(user);

            // Act
            var actual = _sut.RemoveGameFromFavorite(userId, gameId);

            // Assert
            _usersRepositoryMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
            Assert.Equal(expected.GetDataOnSuccess(), actual.GetDataOnSuccess());
        }

        [Fact]
        public void CompareFavorite_ShouldFailWith400_when_comparision_is_invalid()
        {
            // Arrange
            Fixture fix = new Fixture();
            ComparisonRequest comparision = fix.Create<ComparisonRequest>();

            int userId = fix.Create<int>();

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status400BadRequest);

            // Act
            var actual = _sut.CompareFavorite(userId, comparision);

            // Assert

            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public void CompareFavorite_ShouldFailWith404_when_primaryUser_not_found()
        {
            // Arrange
            Fixture fix = new Fixture();
            ComparisonRequest comparision = fix.Build<ComparisonRequest>()
                .With(x => x.Comparison, "union")
                .Create();

            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = null;

            _usersRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(user);

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status404NotFound);

            // Act
            var actual = _sut.CompareFavorite(userId, comparision);

            // Assert

            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public void CompareFavorite_ShouldFailWith400_when_SecondaryUser_not_found()
        {
            // Arrange
            Fixture fix = new Fixture();
            int otherUserId = fix.Create<int>();
            ComparisonRequest comparision = fix.Build<ComparisonRequest>()
                .With(x => x.Comparison, "union")
                .With(x => x.OtherUserId, otherUserId)
                .Create();

            int userId = fix.Create<int>();
            int gameId = fix.Create<int>();
            User? user = fix.Build<User>()
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId } })
                .Create();

            User? otherUser = null;

            _usersRepositoryMock.Setup(x => x.GetById(userId)).Returns(user);
            _usersRepositoryMock.Setup(x => x.GetById(otherUserId)).Returns(otherUser);

            ResultWithData<User> expected = ResultWithData<User>.Failure("", StatusCodes.Status400BadRequest);

            // Act
            var actual = _sut.CompareFavorite(userId, comparision);

            // Assert

            Assert.Equal(expected.ResponseCode, actual.ResponseCode);
        }

        [Fact]
        public void CompareFavorite_Should_return_the_Union_if_comparision_is_union()
        {
            // Arrange
            Fixture fix = new Fixture();


            int userId1 = fix.Create<int>();
            int userId2 = fix.Create<int>();
            int gameId1 = fix.Create<int>();
            int gameId2 = fix.Create<int>();
            int gameId3 = fix.Create<int>();

            ComparisonRequest comparision = fix.Build<ComparisonRequest>()
                .With(x => x.Comparison, "union")
                .With(x => x.OtherUserId, userId2)
                .Create();

            User? user1 = fix.Build<User>()
                .With(x => x.UserId,userId1)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId1 }, new Game { GameId = gameId2 } })
                .Create();
            User? user2 = fix.Build<User>()
                .With(x => x.UserId, userId2)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId2 }, new Game { GameId = gameId3 } })
                .Create();

            _usersRepositoryMock.Setup(x => x.GetById(userId1)).Returns(user1);
            _usersRepositoryMock.Setup(x => x.GetById(userId2)).Returns(user2);

            // Act
            var actual = _sut.CompareFavorite(userId1, comparision);

            // Assert
            Assert.True(actual.GetDataOnSuccess().Games.Count() == 3);
        }

        [Fact]
        public void CompareFavorite_Should_return_the_intersection_if_comparision_is_intersection()
        {
            // Arrange
            Fixture fix = new Fixture();


            int userId1 = fix.Create<int>();
            int userId2 = fix.Create<int>();
            int gameId1 = fix.Create<int>();
            int gameId2 = fix.Create<int>();
            int gameId3 = fix.Create<int>();

            ComparisonRequest comparision = fix.Build<ComparisonRequest>()
                .With(x => x.Comparison, "intersection")
                .With(x => x.OtherUserId, userId2)
                .Create();

            User? user1 = fix.Build<User>()
                .With(x => x.UserId, userId1)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId1 }, new Game { GameId = gameId2 } })
                .Create();
            User? user2 = fix.Build<User>()
                .With(x => x.UserId, userId2)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId2 }, new Game { GameId = gameId3 } })
                .Create();

            _usersRepositoryMock.Setup(x => x.GetById(userId1)).Returns(user1);
            _usersRepositoryMock.Setup(x => x.GetById(userId2)).Returns(user2);

            // Act
            var actual = _sut.CompareFavorite(userId1, comparision);

            // Assert
            Assert.True(actual.GetDataOnSuccess().Games.Count() == 1);
        }

        [Fact]
        public void CompareFavorite_Should_return_the_difference_if_comparision_is_difference()
        {
            // Arrange
            Fixture fix = new Fixture();


            int userId1 = fix.Create<int>();
            int userId2 = fix.Create<int>();
            int gameId1 = fix.Create<int>();
            int gameId2 = fix.Create<int>();
            int gameId3 = fix.Create<int>();

            ComparisonRequest comparision = fix.Build<ComparisonRequest>()
                .With(x => x.Comparison, "intersection")
                .With(x => x.OtherUserId, userId2)
                .Create();

            User? user1 = fix.Build<User>()
                .With(x => x.UserId, userId1)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId1 }, new Game { GameId = gameId2 } })
                .Create();
            User? user2 = fix.Build<User>()
                .With(x => x.UserId, userId2)
                .With(x => x.Games, new HashSet<Game> { new Game { GameId = gameId2 }, new Game { GameId = gameId3 } })
                .Create();

            _usersRepositoryMock.Setup(x => x.GetById(userId1)).Returns(user1);
            _usersRepositoryMock.Setup(x => x.GetById(userId2)).Returns(user2);

            // Act
            var actual = _sut.CompareFavorite(userId1, comparision);

            // Assert
            Assert.True(actual.GetDataOnSuccess().Games.Count() == 1);
        }

    }
}
