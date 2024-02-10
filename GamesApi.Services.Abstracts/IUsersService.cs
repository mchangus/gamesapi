using Games.Core.Models;

namespace Games.Services.Abstracts
{
    public interface IUsersService
    {
        Task<User> Create();
    }
}
