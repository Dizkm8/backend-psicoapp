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
        /// <summary>
        /// Verify if the password matches the hash
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hash">Hash to verify</param>
        /// <returns>True if its match</returns>
        public bool VerifyPassword(string? password, string? hash);
    }
}