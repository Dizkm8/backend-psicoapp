using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get user by credentials
        /// </summary>
        /// <param name="loginUserDto">Entity shape with credentials</param>
        /// <returns>User if it was found, null if not</returns>
        public Task<User?> GetUser(LoginUserDto loginUserDto);
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="id">User id to assign token</param>
        /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
        public Task<string?> GenerateToken(string? id);
        /// <summary>
        /// Async add a new client to the database based on RegisterClientDto shape
        /// </summary>
        /// <param name="registerClientDto">Client to add</param>
        /// <returns>Added user, null it was not added</returns>
        public Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto);
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
    }
}