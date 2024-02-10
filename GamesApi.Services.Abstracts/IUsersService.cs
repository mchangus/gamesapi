using Games.Core.Models;

namespace Games.Services.Abstracts
{
    /// <summary>
    /// The Users interface
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Create a user
        /// </summary>
        /// <returns>Return a <see cref="User"/></returns>
        User Create();

        /// <summary>
        /// Get a user by its Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return a <see cref="User"/></returns>
        User GetById(int userId);
    }
}
