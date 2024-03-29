﻿namespace Games.Domain.Models.Configuration
{
    public class RAWGSettings
    {
        public string? BaseUrl { get; set; }
        public string? RawgApiKey { get; set; }
        public List<string>? GameSearchOrderingOptions { get; set; }
    }
}
