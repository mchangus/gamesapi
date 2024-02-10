using Games.Core.Models;

namespace GamesApi.UsersRepository
{
    public static class UserData
    {
        public static HashSet<User> UserDatabase { get; set; } = default!;
    }
}
