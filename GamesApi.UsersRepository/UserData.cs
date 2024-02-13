using Games.Core.Models;
using System.Collections.Concurrent;

namespace GamesApi.UsersRepository
{
    public static class UserData
    {
        public static ConcurrentDictionary<int, User> UserDatabase { get; set; } = default!;
    }
}
