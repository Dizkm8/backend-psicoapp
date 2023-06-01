using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.UpdateProfileInformation;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get user by credentials
        /// </summary>
        /// <param name="loginUserDto">Entity shape with credentials</param>
        /// <returns>User if it was found, null if not</returns>
        public Task<User?> GetUser(LoginUserDto loginUserDto);
        /// <summary>
        /// Generate a JWT token for the user
        /// </summary>
        /// <param name="id">User id to assign token</param>
        /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
        public Task<string?> GenerateToken(string? id);
        /// <summary>
        /// Async add a new client to the database based on RegisterClientDto shape
        /// </summary>
        /// <param name="registerClientDto">Client to add</param>
        /// <returns>Added user, null it was not added</returns>
        public Task<RegisterClientDto?> AddClient(RegisterClientDto registerClientDto);
        /// <summary>
        /// Asynchronously get a user by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>User if was found, null otherwise</returns>
        public Task<User?> GetUserByEmail(string? email);
        /// <summary>
        /// Asynchronously check if a user exists by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserWithEmail(string? email);
        /// <summary>
        /// Asynchronously check if a user exists by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserById(string? id);
        /// <summary>
        /// Asynchronously check if a user exists by id or email
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="email">User's email</param>
        /// <returns>True if exists, otherwise false</returns>
        public Task<bool> ExistsUserByIdOrEmail(string? id, string? email);
        /// <summary>
        /// Asynchronously search a user exists by id or email
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="email">User's email</param>
        /// <returns>User if was found, null otherwise</returns>
        public Task<User?> GetUserByIdOrEmail(string? id, string? email);
        /// <summary>
        /// Asynchronously update users information contained on Dto shape
        /// </summary>
        /// <param name="newUser">Dto shape with params to update</param>
        /// <returns>Dto with updated user, null if user cannot be found or updated</returns>
        public Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser);
        /// <summary>
        /// Asynchronously get user profile information by their Id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>Profile information Dto shape, null if user cannot be found</returns>
        public Task<ProfileInformationDto?> GetUserProfileInformation();
        /// <summary>
        /// Asynchronously check if an email exists in other user than the one with the id
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="id">User's id</param>
        /// <returns>True if exists. Otherwise false</returns>
        public Task<bool> ExistsEmailInOtherUser(string? email, string? id);
        /// <summary>
        /// Asynchronously check if an email exists in other user than the one with the id extracting them from Token
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>True if exists. Otherwise false</returns>
        public Task<bool> ExistsEmailInOtherUser(string? email);
        /// <summary>
        /// Asynchronously update user password
        /// </summary>
        /// <param name="newPassword">New user's password</param>
        /// <returns>True if password could be changes. Otherwise false</returns>
        public Task<bool> UpdateUserPassword(string? newPassword);
        /// <summary>
        /// Asynchronously check if a user exists by their Id in the token extracted using HttpContext injection
        /// </summary>
        /// <returns>True if its was found. Otherwise null</returns>
        public Task<bool> ExistsUserByToken();
        /// <summary>
        /// Asynchronously check if the provided password matches with the current user's password 
        /// The user is found using the userID extracted from the JWT
        /// </summary>
        /// <param name="password">User's password</param>
        /// <returns>true if the password match. Otherwise false</returns>
        public Task<bool> CheckUsersPasswordUsingToken(string? password);
    }
}