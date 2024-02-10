using Games.Core.Models;
using Games.Domain.Models;

namespace Games.Services.Abstracts
{
    public interface IGamesService
    {
        Task<IEnumerable<Game>> Search(Search search);

        Task AddGameToFavorite(int userId, int gameId);

        Task RemoveGameFromFavorite(int userId, int gameId);

        Task CompareFavorites(int userId, ComparisonRequest comparisonRequest);
    }
}