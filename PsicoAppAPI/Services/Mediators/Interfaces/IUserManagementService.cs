using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Mediators.Interfaces
{
    public interface IUserManagementService
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
        /// <param name="userId">User id to assign token</param>
        /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
        public Task<string?> GenerateToken(string? userId);
    }
}