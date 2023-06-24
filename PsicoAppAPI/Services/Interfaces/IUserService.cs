using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously get a user by credentials
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">User Password</param>
        /// <returns>User if it was found, null if not</returns>
        public Task<User?> GetUserByCredentials(string userId, string password);
        /// <summary>
        /// Async add a new user to the database based on User entity
        /// and using the password previously hashed
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>True if could be added, false if not</returns>
        public Task<bool> AddUser(User? user);
        /// <summary>
        /// Async add a new user with role of client
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>True if could be added, false if not</returns>
        public Task<bool> AddClient(User? user);
        /// <summary>
        /// Async add a new specialist to the system
        /// This creates both entities (user and specialist)
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>True if could be added, false if not</returns>
        public Task<bool> AddSpecialist(User? user);
        /// <summary>
        /// Asynchronously get a user by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>User if was found, null otherwise</returns>
        public Task<User?> GetUserByEmail(string? email);
        /// <summary>
        /// Asynchronously check if a user exists by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserWithEmail(string? email);
        /// <summary>
        /// Asynchronously check if a user exists by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserById(string? id);
        /// <summary>
        /// Asynchronously check if a user exists by id or email
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="email">User's email</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserByIdOrEmail(string? id, string? email);
        /// <summary>
        /// Asynchronously search a user exists by id or email
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="email">User's email</param>
        /// <returns>User if was found, null otherwise</returns>
        public Task<User?> GetUserByIdOrEmail(string? id, string? email);
        /// <summary>
        /// Asynchronously check if an email exists in other user than the one with the id
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="id">User's id</param>
        /// <returns>True if exists. Otherwise false</returns>
        public Task<bool> ExistsEmailInOtherUser(string? email, string? id);
        /// <summary>
        /// Asynchronously get a user by their Id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>The user if it's found, null if not</returns>
        public Task<User?> GetUserById(string? id);
        /// <summary>
        /// Asynchronously update user password by their Id
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <param name="newPassword">User's new Password</param>
        /// <returns>True if password could be changed. Otherwise false</returns>
        public Task<bool> UpdateUserPassword(string? userId, string? newPassword);
        /// <summary>
        /// Asynchronously get the id of the role name as client
        /// </summary>
        /// <returns>Role Id number</returns>
        public Task<int> GetIdOfClientRole();
        /// <summary>
        /// Asynchronously get the id of the role name as Admin
        /// </summary>
        /// <returns>Role Id number</returns>
        public Task<int> GetIdOfAdminRole();
        /// <summary>
        /// Asynchronously get the id of the role name as Specialist
        /// </summary>
        /// <returns>Role Id number</returns>
        public Task<int> GetIdOfSpecialistRole();
        /// <summary>
        /// Asynchronously get the role name by its Id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>RoleId number, -1 if user doesnt exists</returns>
        public Task<int> GetRoleIdInUser(string? userId);
        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>True if could be updated. otherwise false</returns>
        public bool UpdateUser(User? user);
    }
}