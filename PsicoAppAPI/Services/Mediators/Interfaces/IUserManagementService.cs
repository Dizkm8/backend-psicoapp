using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
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
        /// <summary>
        /// Check if exists a user with the UserId in the token and if it is enabled
        /// </summary>
        /// <returns>True if is complete valid. Otherwise false</returns>
        public Task<bool> CheckUserInToken();
        /// <summary>
        /// Asynchronously get the user id in the token and check if the current password
        /// match with the password provided
        /// </summary>
        /// <param name="updatePasswordDto">Dto with the current password</param>
        /// <returns>True if its match. otherwise false</returns>
        public Task<bool> CheckUserCurrentPassword(UpdatePasswordDto updatePasswordDto);
        /// <summary>
        /// Asynchronously get the user id in the token and update the password
        /// </summary>
        /// <param name="updatePasswordDto">Dto with the new password</param>
        /// <returns>True if the password could be updated. Otherwise false</returns>
        public Task<bool> UpdateUserPassword(UpdatePasswordDto updatePasswordDto);
        /// <summary>
        /// Asynchronously get user profile information using the user's Id in the JWT
        /// </summary>
        /// <returns>Profile information Dto shape, null if user cannot be found</returns>
        public Task<ProfileInformationDto?> GetUserProfileInformation();
        /// <summary>
        /// Asynchronously check if the email provided is available to use
        /// not considering the current user's email
        /// </summary>
        /// <param name="dto">Shape of information</param>
        /// <returns>True if it is available. othernise false</returns>
        public Task<bool> CheckEmailUpdatingAvailability(UpdateProfileInformationDto dto);
        /// <summary>
        /// Asynchronously update users information contained on Dto shape using the user's Id in the JWT
        /// </summary>
        /// <param name="newUser">Dto shape with params to update</param>
        /// <returns>Dto with updated user, null if user cannot be found or updated</returns>
        public Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser);
    }
}