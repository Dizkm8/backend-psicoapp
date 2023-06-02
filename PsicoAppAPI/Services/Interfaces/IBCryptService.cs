namespace PsicoAppAPI.Services.Interfaces
{
    public interface IBCryptService
    {
        /// <summary>
        /// Hash the password using BCrypt
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <returns>Password hashed. Null if could not be hashed</returns>
        public string? HashPassword(string? password);
    }
}