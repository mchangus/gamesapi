using Games.Core.Models;
using Newtonsoft.Json;

namespace Games.Domain.Models
{
    [Serializable]
    public class ComparisonResponse: ComparisionBase
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }
}
