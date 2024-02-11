using Games.Core.Models;
using Games.Domain.Models;
using Games.Services.Abstracts;
using GamesApi.Domain.Enums;
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
            User? user = _usersRepository.GetById(userId);

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

        /// <inheritdoc cref="IUsersService.CompareFavorite(int, ComparisonRequest)"/>
        public ResultWithData<ComparisonResponse> CompareFavorite(int userId, ComparisonRequest comparison)
        {
            if (!Enum.TryParse(comparison.Comparison.ToUpper(), out Comparison comparisonCriteria))
            {
                return ResultWithData<ComparisonResponse>.Failure($"{comparison.Comparison} is not a valid comparison.", StatusCodes.Status400BadRequest);
            }

            User? primaryUser = _usersRepository.GetById(userId);

            if (primaryUser is null)
            {
                return ResultWithData<ComparisonResponse>.Failure($"User with Id {userId} does not exist.", StatusCodes.Status404NotFound);
            }

            User? secondaryUser = _usersRepository.GetById(comparison.OtherUserId);

            if (secondaryUser is null)
            {
                return ResultWithData<ComparisonResponse>.Failure($"User with Id {comparison.OtherUserId} does not exist.", StatusCodes.Status400BadRequest);
            }

            IEnumerable<Game>? favoritesComparison = null;

            switch (comparisonCriteria)
            {
                case Comparison.UNION:
                    favoritesComparison = FavoritesUnion(primaryUser, secondaryUser);
                    break;
                case Comparison.INTERSECTION:
                    favoritesComparison = FavoritesIntersection(primaryUser, secondaryUser);
                    break;
                case Comparison.DIFFERENCE:
                    favoritesComparison = FavoritesDifference(primaryUser, secondaryUser);
                    break;
            }

            ComparisonResponse comparisonResponse = new ComparisonResponse 
            { 
             OtherUserId = comparison.OtherUserId,
             UserId = userId,
             Comparison = comparison.Comparison,
             Games = favoritesComparison
            };

            return ResultWithData<ComparisonResponse>.Success(comparisonResponse);
        }

        /// <inheritdoc cref="IUsersService.Create(int, int)"/>
        public User Create()
        {
            return _usersRepository.Create();
        }

        /// <inheritdoc cref="IUsersService.GetById(int)"/>
        public User? GetById(int userId)
        {
            return _usersRepository.GetById(userId);
        }

        /// <inheritdoc cref="IUsersService.RemoveGameFromFavorite(int, int)"/>
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


        private IEnumerable<Game> FavoritesUnion(User primaryUser, User secondaryUser)
        {
            return primaryUser.Games.Union(secondaryUser.Games).Distinct();

        }

        private IEnumerable<Game> FavoritesIntersection(User primaryUser, User secondaryUser)
        {
            return primaryUser.Games.Intersect(secondaryUser.Games);

        }

        private IEnumerable<Game> FavoritesDifference(User primaryUser, User secondaryUser)
        {
            return secondaryUser.Games.Except(primaryUser.Games);
        }
    }
}