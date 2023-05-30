using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IClientService
    {
        public Task<Client?> GetClient(LoginUserDto loginUserDto);

        public string? GenerateToken(string? id);
    }
}