using FluentValidation.Results;
using Games.Core.Models;
using Games.Domain.Models;
using Games.Domain.Models.Configuration;
using Games.Domain.Validators;
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
        public GamesController(ILogger<GamesController> logger, IOptions<RAWGSettings> searchSettings)
        {
            _logger = logger;
            _searchSettings = searchSettings.Value;
        }

        [HttpGet(Name = "games")]
        public IActionResult GetGames([FromQuery]Search search)
        {
            SearchValidator validator = new(_searchSettings.GameSearchOrderingOptions);
            ValidationResult result = validator.Validate(search);

            if (!result.IsValid) 
            {
                return BadRequest(result.Errors.FirstOrDefault()?.ErrorMessage);
            }

            return  Ok(new Game[0]);
        }
    }
}