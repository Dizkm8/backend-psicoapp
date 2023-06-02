using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="id">User id to assign token</param>
        /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
        public Task<string?> GenerateToken(string? id);
        /// <summary>
        /// Get the role of the user based on their Id
        /// </summary>
        /// <returns>Role of the user, null if the user doesn't exists</returns>
        public Task<string?> GetUserRole(string userId);
        /// <summary>
        /// Get the user id from the token using HttpContext
        /// </summary>
        /// <returns>string with the Id. null if something gone wrong</returns>
        public string? GetUserIdInToken();
        /// <summary>
        /// Get the user role from the token using HttpContext
        /// </summary>
        /// <returns>string with the Role. null if something gone wrong</returns>
        public string? GetUserRoleInToken();
        /// <summary>
        /// Get the userId from the token, found a user in Repository and return it
        /// </summary>
        /// <returns>
        /// User found. If the userId or Token are invalid 
        /// or simply user doesn't exists on repository return null
        ///</returns>
        public Task<User?> GetUserUsingToken();
        /// <summary>
        /// Asynchronously check if the provided password matches with the current user's password 
        /// The user is found using the userID extracted from the JWT
        /// </summary>
        /// <param name="password">User's password</param>
        /// <returns>true if the password match. Otherwise false</returns>
        public Task<bool> CheckUsersPasswordUsingToken(string? password);
    }
}