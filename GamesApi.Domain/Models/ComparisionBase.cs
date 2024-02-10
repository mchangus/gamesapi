using Newtonsoft.Json;

namespace Games.Domain.Models
{
    [Serializable]
    public abstract class ComparisionBase
    {
        [JsonProperty("otherUserId")]
        public int OtherUserId { get; set; }

        [JsonProperty("comparison")]
        public string Comparison { get; set; }
    }
}
