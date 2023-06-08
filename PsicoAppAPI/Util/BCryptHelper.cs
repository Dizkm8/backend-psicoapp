namespace PsicoAppAPI.Util
{
    public class BCryptHelper
    {
        /// <summary>
        /// Hash the password using BCrypt
        /// </summary>
        /// <param name="password">password to hash</param>
        /// <returns>Password hashed. Null if could not be hashed</returns>
        public static string? HashPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        /// <summary>
        /// Verify if the password matches the hash
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hash">Hash to verify</param>
        /// <returns>True if its match</returns>
        public static bool VerifyPassword(string? password, string? hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}