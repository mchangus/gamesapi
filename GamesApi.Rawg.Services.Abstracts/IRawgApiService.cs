using Games.Core.Models;

namespace GamesApi.Rawg.Services.Abstracts
{
    /// <summary>
    /// The RawgApiService Interface
    /// </summary>
    public interface IRawgApiService
    {
        /// <summary>
        /// Get all games suing filter and ordering parameter
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ordering"></param>
        /// <returns>A collection of Games"/></returns>
        Task<IEnumerable<Game>> GetGamesAsync(string query, string ordering);             
    }
}