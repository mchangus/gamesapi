using Games.Core.Models;

namespace GamesApi.UsersRepository.Abstracts
{
    /// <summary>
    /// The User repository interface
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <returns><see cref="User"/></returns>
        User Create();

        /// <summary>
        /// Get an user given its id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="User"/></returns>
        User? GetById(int userId);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);
    }
}