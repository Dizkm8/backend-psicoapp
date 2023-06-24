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
        /// <summary>
        /// Asynchronously get a user by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>User if was found, null otherwise</returns>
        public Task<User?> GetUserByEmail(string email);
        /// <summary>
        /// Asynchronously check if a user exists by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserWithEmail(string email);
        /// <summary>
        /// Asynchronously search a user exists by id or email
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="email">User's email</param>
        /// <returns>User if was found, null otherwise</returns>
        public Task<User?> GetUserByIdOrEmail(string id, string email);
        /// <summary>
        /// Asynchronously check if a user exists by id or email
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="email">User's email</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserByIdOrEmail(string id, string email);
        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>User updated. null if something gone wrong</returns>
        public User UpdateUserAndSaveChanges(User user);
        /// <summary>
        /// Get all the users in the system
        /// </summary>
        /// <returns>List with Users</returns>
        public Task<List<User>> GetAllUsers();
    }
}