using PsicoAppAPI.DTOs;
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
        /// Async add a new client to the database based on User entity
        /// and using the password previously hashed
        /// </summary>
        /// <param name="user">Client to add</param>
        /// <param name="bCryptService">IBcryptService instance</param>
        /// <returns>True if could be added, false if not</returns>
        public Task<bool> AddClient(User? user, IBCryptService bCryptService);
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
        /// Asynchronously update users information contained on Dto shape by their Id
        /// </summary>
        /// <param name="newUser">Dto shape with params to update</param>
        /// <param name="userId">user's Id</param>
        /// <param name="mapperService">IMapperService instance</param>
        /// <returns>Dto with updated user, null if user cannot be found or updated</returns>
        public Task<UpdateProfileInformationDto?> UpdateProfileInformation(UpdateProfileInformationDto newUser, string? userId, IMapperService mapperService);
        /// <summary>
        /// Asynchronously get user profile information by their Id and role
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <param name="userRole">User's Role</param>
        /// <param name="mapperService">IMapperService instance</param>
        /// <returns>Profile information Dto shape, null if user cannot be found</returns>
        public Task<ProfileInformationDto?> GetUserProfileInformation(string? userId, string? userRole, IMapperService mapperService);
        /// <summary>
        /// Asynchronously check if an email exists in other user than the one with the id
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="id">User's id</param>
        /// <returns>True if exists. Otherwise false</returns>
        public Task<bool> ExistsEmailInOtherUser(string? email, string? id);
        /// <summary>
        /// Asynchronously get a user by their Id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>The user if it's found, null if not</returns>
        public Task<User?> GetUserById(string? id);
        /// <summary>
        /// Asynchronously update user password by their Id
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <param name="newPassword">User's new Password</param>
        /// <param name="bCryptService">IBcryptService instance</param>
        /// <returns>True if password could be changed. Otherwise false</returns>
        public Task<bool> UpdateUserPassword(string? userId, string? newPassword, IBCryptService bCryptService);
        
    }
}