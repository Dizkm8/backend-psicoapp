using PsicoAppAPI.DTOs;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Mediators.Interfaces
{
    public interface IUserManagementService
    {
        /// <summary>
        /// Get user by credentials
        /// </summary>
        /// <param name="loginUserDto">Entity shape with credentials</param>
        /// <returns>User if it was found, null if not</returns>
        public Task<bool> CheckCredentials(LoginUserDto loginUserDto);
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="userId">User id to assign token</param>
        /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
        public Task<string?> GenerateToken(LoginUserDto loginUserDto);
        /// <summary>
        /// Asynchronously check if a email is available to use
        /// </summary>
        /// <param name="registerClientDto">User shape Dto to email validation</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> CheckEmailAvailability(RegisterClientDto registerClientDto);
        /// <summary>
        /// Asynchronously check if a user exists by id
        /// </summary>
        /// <param name="registerClientDto">User shape Dto to Id validation</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> CheckUserIdAvailability(RegisterClientDto registerClientDto);
        /// <summary>
        /// Async add a new client to the database based on RegisterClientDto shape
        /// </summary>
        /// <param name="registerClientDto">Client to add</param>
        /// <returns>Added user, null it was not added</returns>
        public Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto);
    }
}