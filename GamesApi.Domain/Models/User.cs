﻿using System.Text.Json.Serialization;

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

        public HashSet<Game> Games { get; init; } = new HashSet<Game>();

    }
}