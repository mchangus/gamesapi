using Newtonsoft.Json;

namespace GamesApi.Domain.Models.Requests
{
    public class ComparisonRequestBody
    {
        [JsonProperty("otherUserId")]
        public int OtherUserId { get; set; }

        [JsonProperty("comparison")]
        public string Comparison { get; set; }
    }
}
