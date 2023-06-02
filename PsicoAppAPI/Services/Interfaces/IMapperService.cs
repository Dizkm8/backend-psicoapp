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
        /// <summary>
        /// Maps the attributes of a UpdateProfileInformationDto to a User
        /// The Dto dont have all the attributes of the User,
        /// so map only affects the attributes that the Dto have
        /// This function dont use AutoMapper to avoid overwrite existing attributes
        /// </summary>
        /// <param name="profileInformationDto">Source Dto</param>
        /// <param name="user">User destination</param>
        /// <returns>User mapped</returns>
        public User MapAttributesToUser(UpdateProfileInformationDto profileInformationDto, User user);
    }
}