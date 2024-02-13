using Games.Core.Models;
using GamesApi.UsersRepository.Abstracts;

namespace GamesApi.UsersRepository
{
    /// <summary>
    /// The user repository class
    /// </summary>
    public class UsersRepository : IUsersRepository
    {        
        public UsersRepository()
        {
            UserData.UserDatabase ??= new System.Collections.Concurrent.ConcurrentDictionary<int, User>();

        }
        /// <inheritdoc cref="IUsersRepository.Create"/>
        public User Create()
        {            
            User user = UserData.UserDatabase.Any()
                        ? UserData.UserDatabase.Last().Value.CloneEmpty()
                        : new User();

            user.Increment();

            UserData.UserDatabase.TryAdd(user.UserId, user);

            return user;
        }

        /// <inheritdoc cref="IUsersRepository.GetById"/>
        public User? GetById(int userId)
        {
            return UserData.UserDatabase.FirstOrDefault(x => x.Key == userId).Value;
        }

        public void UpdateUser(User user)
        {
            UserData.UserDatabase.TryUpdate(user.UserId, user, user);
        }
    }
}