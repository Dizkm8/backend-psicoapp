using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class BCryptService : IBCryptService
    {
        public string? HashPassword(string? password)
        {
            if(string.IsNullOrEmpty(password)) return null;
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}