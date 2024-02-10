using Games.Core.Models;
using Games.Domain.Models;
using Games.Services.Abstracts;
using GamesApi.Rawg.Services.Abstracts;

namespace Games.Services
{
    /// <summary>
    /// 
    /// </summary>
    internal class GamesService : IGamesService
    {
        private readonly IRawgApiService _rawgApiService;
        public GamesService(IRawgApiService rawgApiService)
        {
            _rawgApiService = rawgApiService;
        }
        public async Task AddGameToFavoritesAsync(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<ComparisonResponse> CompareFavoritesAsync(int userId, ComparisonRequest comparisonRequest)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveGameFromFavoritesAsync(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> SearchAsync(Search search)
        {
            ArgumentNullException.ThrowIfNull(search);

            return await _rawgApiService.GetGamesAsync(search.Query, search.Sort);
        }

    }
}