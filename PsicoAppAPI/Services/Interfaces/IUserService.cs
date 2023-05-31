using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetUser(LoginUserDto loginUserDto);

        public Task<string?> GenerateToken(string? id);
    }
}