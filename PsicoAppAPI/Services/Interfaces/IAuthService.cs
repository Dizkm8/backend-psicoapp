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
    }
}