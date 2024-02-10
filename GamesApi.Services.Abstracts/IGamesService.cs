using Games.Core.Models;
using Games.Domain.Models;

namespace Games.Services.Abstracts
{
    /// <summary>
    /// The Game Service Interface
    /// </summary>
    public interface IGamesService
    {
        /// <summary>
        /// Search games base of a serach criteria speficy in parameter
        /// </summary>
        /// <param name="search"></param>
        /// <returns>A collection of Games</returns>
        Task<IEnumerable<Game>> SearchAsync(Search search);

        /// <summary>
        /// Add a game to user's favorites game collection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        Task AddGameToFavoritesAsync(int userId, int gameId);

        /// <summary>
        /// Remove a game from the user's favorites game collection
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        Task RemoveGameFromFavoritesAsync(int userId, int gameId);

        /// <summary>
        /// Compare two users favorite games collection using Comparison defined in parameters
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="comparisonRequest"></param>
        /// <returns><see cref="ComparisonResponse"/></returns>
        Task<ComparisonResponse> CompareFavoritesAsync(int userId, ComparisonRequest comparisonRequest);
    }
}