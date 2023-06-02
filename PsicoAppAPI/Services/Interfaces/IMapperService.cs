using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IMapperService
    {
        /// <summary>
        /// Maps a RegisterClientDto to a User
        /// </summary>
        /// <param name="registerClientDto">Dto to map</param>
        /// <returns>User mapped. null if cannot be mapped</returns>
        public User? MapToUser(RegisterClientDto? registerClientDto);
    }
}