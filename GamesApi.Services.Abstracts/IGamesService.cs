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
    }
}