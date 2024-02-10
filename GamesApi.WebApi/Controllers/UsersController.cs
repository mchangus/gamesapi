using Games.Core.Models;
using Games.Domain.Models;
using Games.Services.Abstracts;
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

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public void AddGameToFavorite([FromBody] int gameId)
        {
            //TODO: Possibly create a new result to know if the game is not there return 400
            // if user not there return 404
            // if game already added return 409

        }

        [HttpDelete]
        [Route("{userId}/games/{gameId}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        public void RemoveGameFromFavorite([FromRoute] int userId, int gameId)
        {
            //TODO
        }

        [HttpPost]
        [Route("{userId}/comparison")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(ComparisonResponse))]
        public void CompareFavorite([FromRoute] int userId, [FromBody] ComparisonRequest comparison)
        {
            //TODO:
       /*     Return a `400 Bad Request` response if a user matching `otherUserId` (in the request body) does not exist or if `comparison` (in the request body) is invalid.

              Return a `404 Not Found` response if a user matching `userId` (in the URL) does not exist.*/

        }

    }
}