namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="userId">User id to assign token</param>
        /// <param name="userRole">User role to assign inside token</param>
        /// <param name="userName">User name to assign inside token</param>
        /// <returns>string with the token</returns>
        public string? GenerateToken(string userId, string userRole, string userName);
        /// <summary>
        /// Get the user id from the token using HttpContext
        /// </summary>
        /// <returns>string with the Id. null if something gone wrong</returns>
        public string? GetUserIdInToken();
        /// <summary>
        /// Get the user role from the token using HttpContext
        /// </summary>
        /// <returns>string with the Role.  -1 if cannot be obtained</returns>
        public int GetUserRoleInToken();
    }
}