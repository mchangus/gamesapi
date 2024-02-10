using Games.Core.Models;
using Games.Domain.Models;
using Games.Services.Abstracts;

namespace Games.Services
{
    internal class GamesService : IGamesService
    {
        public Task AddGameToFavorite(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public Task CompareFavorites(int userId, ComparisonRequest comparisonRequest)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGameFromFavorite(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> Search(Search search)
        {
            throw new NotImplementedException();
        }
    }
}