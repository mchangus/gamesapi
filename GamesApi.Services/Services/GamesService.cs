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

        /// <inheritdoc cref="IGamesService.SearchAsync(int, int)"/>

        public async Task<IEnumerable<Game>> SearchAsync(Search search)
        {
            ArgumentNullException.ThrowIfNull(search);

            return await _rawgApiService.GetGamesAsync(search.Query, search.Sort);
        }

    }
}