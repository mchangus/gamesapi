using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Games.Domain.Models
{
    [BindProperties]
    public class Search
    {
        [BindProperty(Name = "q", SupportsGet = true)]
        // TODO: [Required]
        public string? Query { get; set; }

        [BindProperty(Name = "sort", SupportsGet = true)]
        public string? Sort { get; set; }
    }
}
