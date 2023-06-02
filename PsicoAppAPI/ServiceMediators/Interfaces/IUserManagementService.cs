using PsicoAppAPI.DTOs;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.ServiceMediators.Interfaces
{
    public interface IUserManagementService
    {
        /// <summary>
        /// Get user service
        /// </summary>
        /// <value>IUserService</value>
        public IUserService UserService { get; }
        /// <summary>
        /// Get auth service
        /// </summary>
        /// <value>IAuthService</value>
        public IAuthService AuthService { get; }
        /// <summary>
        /// Asynchronously check if a user exists by their Id in the token extracted using HttpContext
        /// </summary>
        /// <returns>True if its was found. Otherwise null</returns>
        public Task<bool> ExistsUserByToken();
        /// <summary>
        /// Asynchronously update user password
        /// </summary>
        /// <param name="newPassword">New user's password</param>
        /// <returns>True if password could be changed. Otherwise false</returns>
        public Task<bool> UpdateUserPassword(string? newPassword);
        /// <summary>
        /// Asynchronously check if an email exists in other user than the one with the id
        /// The userId is extracted from the JWT 
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>True if exists. Otherwise false</returns>
        public Task<bool> ExistsEmailInOtherUser(string? email);
        /// <summary>
        /// Asynchronously get user profile information using the user's Id in the JWT
        /// </summary>
        /// <returns>Profile information Dto shape, null if user cannot be found</returns>
        public Task<ProfileInformationDto?> GetUserProfileInformation();
        /// <summary>
        /// Asynchronously update users information contained on Dto shape using the user's Id in the JWT
        /// </summary>
        /// <param name="newUser">Dto shape with params to update</param>
        /// <returns>Dto with updated user, null if user cannot be found or updated</returns>
        public Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser);
        /// <summary>
        /// Async add a new client to the database based on RegisterClientDto shape
        /// </summary>
        /// <param name="registerClientDto">Client to add</param>
        /// <returns>Added user, null it was not added</returns>
        public Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto);
        /// <summary>
        /// Asynchronously check if the provided password matches with the current user's password.
        /// The user is found using the userId extracted from the JWT
        /// </summary>
        /// <param name="password">User's password</param>
        /// <returns>true if the password match. Otherwise false</returns>
        public Task<bool> CheckUsersPasswordUsingToken(string? password);

        
    }
}