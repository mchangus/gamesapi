using AutoFixture;
using Games.Core.Models;
using Games.Services;
using Games.Services.Abstracts;
using GamesApi.Rawg.Services.Abstracts;
using GamesApi.UsersRepository.Abstracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Create_ShouldCallRepo_and_Return_Created_User ()
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
        public void GetByIs_ShouldCallRepo_and_Return_User()
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


    }
}
