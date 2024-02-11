using Games.Core.Models;

namespace GamesApi.UsersRepository
{
    public static class UserData
    {
        //TODO: Check if List is better HashSet or List for the operations we are doing
        public static HashSet<User> UserDatabase { get; set; } = default!;
    }
}
