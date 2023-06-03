using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class BCryptService : IBCryptService
    {
        public BCryptService()
        {
        }

        public string? HashPassword(string? password)
        {
            if(string.IsNullOrEmpty(password)) return null;
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string? password, string? hash)
        {
            if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}