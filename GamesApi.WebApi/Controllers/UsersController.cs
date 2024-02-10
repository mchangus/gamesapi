using Games.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Games.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesErrorResponseType(type: typeof(User))]        
        public async Task<User> CreateUser([FromBody] User user)
        {
            return user;
        }
    }
}