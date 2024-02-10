using System.Text.Json.Serialization;

namespace Games.Core.Models
{
    /// <summary>
    /// The User Class
    /// </summary>
    [Serializable]
    public class User
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        public List<Game> Games { get; set; } = default!;

    }
}