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
            UserData.UserDatabase ??= new HashSet<User>();
        }
        /// <inheritdoc cref="IUsersRepository.Create"/>
        public User Create()
        {            
            User user = new();
            if (!UserData.UserDatabase.Any())
            {
                user.UserId = 1;
            }
            else
            {
                user.UserId =  UserData.UserDatabase.Last().UserId + 1;
            }

            UserData.UserDatabase.Add(user);

            return user;
        }

        /// <inheritdoc cref="IUsersRepository.GetById"/>
        public User? GetById(int userId)
        {
            return UserData.UserDatabase.FirstOrDefault(x => x.UserId == userId);
        }

        public void UpdateUser(User user)
        {
            UserData.UserDatabase.RemoveWhere(x => x.UserId == user.UserId);

            UserData.UserDatabase.Add(user);
        }
    }
}