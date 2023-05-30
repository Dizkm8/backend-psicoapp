using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Asynchronously get a user by their Id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>The user if it's found, null if not</returns>
        public Task<User?> GetUserById(string id);

        /// <summary>
        /// Asynchronously get a user by their Id and password
        /// </summary>
        /// <param name="id">userId</param>
        /// <param name="password">user password</param>
        /// <returns>User if it's found, null if not</returns>
        public Task<User?> GetUserByCredentials(string id, string password);
        /// <summary>
        ///  Add a User to database
        /// </summary>
        /// <param name="User">user to add</param>
        /// <returns>True if could be added</returns>
        public bool AddUser(User user);
        /// <summary>
        /// Asynchronous add a user to database and save the changes
        /// </summary>
        /// <param name="User">User to add</param>
        /// <returns>True if could be added</returns>
        public Task<bool> AddUserAndSaveChanges(User user);
        /// <summary>
        /// Save the changes in the database
        /// </summary>
        /// <returns>True if any change was made</returns>
        public bool SaveChanges();
        /// <summary>
        /// Asynchronous save the changes in the database
        /// </summary>
        /// <returns>True if any change was made</returns>
        public Task<bool> SaveChangesAsync();
        /// <summary>
        /// Check if a user exists in the database
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>True if exists</returns>
        public Task<bool> UserExists(string id);
        /// <summary>
        /// Check if a user exists in the database by their Id
        /// </summary>
        /// <param name="User">User to get Id</param>
        /// <returns>True if exists</returns>
        public Task<bool> UserExists(User user);
    }
}