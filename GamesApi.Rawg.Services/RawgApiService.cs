using AutoMapper;
using Core = Games.Core.Models;
using Games.Domain.Models.Configuration;
using GamesApi.Domain.Constants;
using GamesApi.Rawg.Services.Abstracts;
using GamesApi.Rawg.Services.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Web;

namespace GamesApi.Rawg.Services
{
    internal class RawgApiService : IRawgApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RAWGSettings _rawgSettings;
        private readonly IMapper _mapper;

        private readonly Func<string?, string?, string, string?, string> _gamesUrl = (baseUrl, key, query, ordering) =>
            string.IsNullOrEmpty(ordering)
            ? $"{baseUrl}api/games?key={key}&search={HttpUtility.UrlEncode(query)}"
            : $"{baseUrl}api/games?key={key}&search={HttpUtility.UrlEncode(query)}&ordering={HttpUtility.UrlEncode(ordering)}";

        public RawgApiService(IHttpClientFactory httpClientFactory,
            IOptions<RAWGSettings> rawgSettings,
            IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _rawgSettings = rawgSettings.Value;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IRawgApiService.GetGamesAsync" />
        public async Task<IEnumerable<Core.Game>> GetGamesAsync(string query, string? ordering)
        {
            ArgumentNullException.ThrowIfNull(query);

            var httpClient = _httpClientFactory.CreateClient(SettingsContants.RawgHttClientName);

            var url = _gamesUrl(_rawgSettings.BaseUrl, _rawgSettings.RawgApiKey, query, ordering);

            HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            GamesResponse gamesResponse = JsonConvert.DeserializeObject<GamesResponse>(content);

            var games = _mapper.Map<IEnumerable<Core.Game>>(gamesResponse.Results);

            return games;
        }

        /// <inheritdoc cref="IRawgApiService.GetGamesAsync" />
        public async Task<Core.Game> GetGameByIdAsync(int gameId)
        {

            var httpClient = _httpClientFactory.CreateClient(SettingsContants.RawgHttClientName);

            var url = $"{_rawgSettings.BaseUrl}api/games/{gameId}";

            HttpResponseMessage response = await httpClient.GetAsync(new Uri(url));

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            };

            string content = await response.Content.ReadAsStringAsync();

            Game rawgGame = JsonConvert.DeserializeObject<Game>(content);

            var game = _mapper.Map<Core.Game>(rawgGame);

            return game;
        }

    }
}