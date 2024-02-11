using Games.Core.Models;
using Games.Domain.Models;
using Games.Services.Abstracts;
using GamesApi.Domain.Models.Requests;
using GamesApi.Domain.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUsersService _usersService;
        public UsersController(ILogger<UsersController> logger,
            IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(User))]
        [Route("{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            User? user = _usersService.GetById(id);

            if (user is null) { return NotFound(); }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(User))]
        public IActionResult CreateUser()
        {
            User user = _usersService.Create();

            return new CreatedResult("/users",user);
        }

        [HttpPost]
        [Route("{userId}/games")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddGameToFavorite([FromRoute] int userId, [FromBody] AddFavoriteRequestBody body)
        {
            ResultWithData<User> resultWithData = await _usersService.AddGameToFavoriteAsync(userId, body.GameId);
            
            if (!resultWithData.Succeeded) 
            {
                return resultWithData.ResponseCode switch
                {
                    StatusCodes.Status400BadRequest => BadRequest(resultWithData.Message),
                    StatusCodes.Status404NotFound => NotFound(resultWithData.Message),
                    StatusCodes.Status409Conflict => Conflict(resultWithData.Message),
                    _ => BadRequest(),
                };
            }

            return NoContent();

        }

        [HttpDelete]
        [Route("{userId}/games/{gameId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public IActionResult RemoveGameFromFavorite([FromRoute] int userId, int gameId)
        {

            ResultWithData<User> resultWithData = _usersService.RemoveGameFromFavorite(userId, gameId);

            if (!resultWithData.Succeeded)
            {
                return resultWithData.ResponseCode switch
                {
                    StatusCodes.Status400BadRequest => BadRequest(resultWithData.Message),
                    StatusCodes.Status404NotFound => NotFound(resultWithData.Message),
                    StatusCodes.Status409Conflict => Conflict(resultWithData.Message),
                    _ => BadRequest(),
                };
            }

            return NoContent();
        }

        [HttpPost]
        [Route("{userId}/comparison")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(ComparisonResponse))]
        public IActionResult CompareFavorite([FromRoute] int userId, [FromBody] ComparisonRequest comparison)
        {
            ResultWithData<ComparisonResponse> resultWithData = _usersService.CompareFavorite(userId, comparison);

            if (!resultWithData.Succeeded)
            {
                return resultWithData.ResponseCode switch
                {
                    StatusCodes.Status400BadRequest => BadRequest(resultWithData.Message),
                    StatusCodes.Status404NotFound => NotFound(resultWithData.Message),
                    StatusCodes.Status409Conflict => Conflict(resultWithData.Message),
                    _ => BadRequest(),
                };
            }

            return Ok(resultWithData.GetDataOnSuccess());
        }

    }
}