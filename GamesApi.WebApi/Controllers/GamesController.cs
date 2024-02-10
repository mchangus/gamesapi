using FluentValidation.Results;
using Games.Core.Models;
using Games.Domain.Models;
using Games.Domain.Models.Configuration;
using Games.Domain.Validators;
using Games.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly RAWGSettings _searchSettings;

        private readonly IGamesService _gamesService;
        public GamesController(ILogger<GamesController> logger, 
            IOptions<RAWGSettings> searchSettings,
            IGamesService gameService)
        {
            _logger = logger;
            _searchSettings = searchSettings.Value;
            _gamesService = gameService;
        }

        [HttpGet(Name = "games")]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK, Type = typeof(IEnumerable<Game>))]        
        public async Task<IActionResult> GetGames([FromQuery]Search search)
        {
            SearchValidator validator = new(_searchSettings.GameSearchOrderingOptions);
            ValidationResult result = validator.Validate(search);

            if (!result.IsValid) 
            {
                return BadRequest(result.Errors.FirstOrDefault()?.ErrorMessage);
            }

            IEnumerable<Game> games = await _gamesService.SearchAsync(search);

            return Ok(games);
        }
    }
}