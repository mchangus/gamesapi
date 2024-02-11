using Games.Core.Models;
using Games.Domain.Models;
using GamesApi.Domain.Models.Results;

namespace Games.Services.Abstracts
{
    /// <summary>
    /// The Users interface
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Add a game to the favority list of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns>an instance of <see cref="ResultWithData{T}"/></returns>
        Task<ResultWithData<User>> AddGameToFavoriteAsync(int userId, int gameId);

        /// <summary>
        /// Compare favorites from two users based in comparision soecification
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="comparison"></param>
        /// <returns>an instance of <see cref="ResultWithData{T}"/></returns>
        ResultWithData<ComparisonResponse> CompareFavorite(int userId, ComparisonRequest comparison);

        /// <summary>
        /// Create a user
        /// </summary>
        /// <returns>Return a <see cref="User"/></returns>
        User Create();

        /// <summary>
        /// Get a user by its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return a <see cref="User"/></returns>
        User GetById(int userId);

        /// <summary>
        /// Remove a game to the favority list of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        ResultWithData<User> RemoveGameFromFavorite(int userId, int gameId);
    }
}
