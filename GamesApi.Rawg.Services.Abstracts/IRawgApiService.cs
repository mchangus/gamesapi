using Games.Core.Models;

namespace GamesApi.Rawg.Services.Abstracts
{
    /// <summary>
    /// The RawgApiService Interface
    /// </summary>
    public interface IRawgApiService
    {
        /// <summary>
        /// Get all games using filter and ordering parameter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ordering"></param>
        /// <returns>A <see cref="IEnumerable{Game}"/>"/></returns>
        Task<IEnumerable<Game>> GetGamesAsync(string query, string ordering);

        /// <summary>
        /// Get a game given the id
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ordering"></param>
        /// <returns>a <see cref="Game"/>"/></returns>
        Task<Game> GetGameByIdAsync(int gameId);
    }
}