namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="userId">User id to assign token</param>
        /// <param name="userRole">User role to assign inside token</param>
        /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
        public string? GenerateToken(string userId, string userRole);
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
    }
}