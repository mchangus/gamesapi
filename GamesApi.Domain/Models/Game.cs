using System.Text.Json.Serialization;

namespace Games.Core.Models
{
    /// <summary>
    /// The Game Class
    /// </summary>
    [Serializable]
    public class Game
    {
        [JsonPropertyName("gameId")]
        public int GameId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("added")]
        public int Added { get; set; }

        [JsonPropertyName("metacritic")]
        public int? Metacritic { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("released")]
        public string Released { get; set; }

        [JsonPropertyName("updated")]
        public DateTime? Updated { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Game other) return false;

            return other.GameId == GameId;
        }

        public override int GetHashCode()
        {
            return GameId.GetHashCode();
        }
    }
}
