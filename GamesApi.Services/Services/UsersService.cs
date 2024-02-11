using Games.Core.Models;
using Games.Services.Abstracts;
using GamesApi.Domain.Models.Results;
using GamesApi.Rawg.Services.Abstracts;
using GamesApi.UsersRepository.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Games.Services
{
    /// <summary>
    /// The User Service Class
    /// </summary>
    internal class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        private readonly IRawgApiService _rwgApiService;

        public UsersService(IUsersRepository usersRepository,
            IRawgApiService rawgApiService)
        {
            _usersRepository = usersRepository;

            _rwgApiService = rawgApiService;

        }

        /// <inheritdoc cref="IUsersService.AddGameToFavoriteAsync(int, int)"/>
        public async Task<ResultWithData<User>> AddGameToFavoriteAsync(int userId, int gameId)
        {
            User? user  = _usersRepository.GetById(userId);

            if (user is null) 
            {
                return ResultWithData<User>.Failure($"User with Id {userId} does not exist.", StatusCodes.Status404NotFound);
            }

            if (user.Games.Any(x => x.GameId == gameId))
            {
                return ResultWithData<User>.Failure($"Game with Id {gameId} is already in favorites!", StatusCodes.Status409Conflict);
            }

            Game game = await _rwgApiService.GetGameByIdAsync(gameId);

            if (game is null)
            {
                return ResultWithData<User>.Failure($"Game with Id {gameId} does not exist.", StatusCodes.Status400BadRequest);
            }

            user.Games.Add(game);

            _usersRepository.UpdateUser(user);

            return ResultWithData<User>.Success(user);
        }

        public User Create()
        {
            return _usersRepository.Create();
        }

        public User? GetById(int userId)
        {
            return _usersRepository.GetById(userId);
        }

        public ResultWithData<User> RemoveGameFromFavorite(int userId, int gameId)
        {
            User? user = _usersRepository.GetById(userId);

            if (user is null)
            {
                return ResultWithData<User>.Failure($"User with Id {userId} does not exist.", StatusCodes.Status400BadRequest);
            }

            if (!user.Games.Any(x => x.GameId == gameId))
            {
                return ResultWithData<User>.Failure($"Game with Id {gameId} was not found in favorites.", StatusCodes.Status404NotFound);
            }

            user.Games.RemoveWhere(x => x.GameId == gameId);

            _usersRepository.UpdateUser(user);

            return ResultWithData<User>.Success(user);

        }
    }
}
