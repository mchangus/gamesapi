using Games.Core.Models;
using Games.Services.Abstracts;
using GamesApi.UsersRepository.Abstracts;

namespace Games.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public User Create()
        {
            return _usersRepository.Create();
        }

        public User GetById(int userId)
        {
            return _usersRepository.GetById(userId);
        }
    }
}
