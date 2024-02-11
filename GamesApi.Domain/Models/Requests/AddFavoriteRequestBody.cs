using Newtonsoft.Json;

namespace GamesApi.Domain.Models.Requests
{
    public class AddFavoriteRequestBody
    {
        [JsonProperty("gameId")]
        public int GameId { get; set; }
    }
}
