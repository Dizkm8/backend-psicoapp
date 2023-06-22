using PsicoAppAPI.Models;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IAuthManagementService
{
    /// <summary>
    /// Get a enabled user by userId extracted from token
    /// </summary>
    /// <returns>A user enabled is could be found. otherwise false</returns>
    public Task<User?> GetUserEnabledFromToken();
    /// <summary>
    /// Check if exists a user and if it is enabled 
    /// using their usedId extracted from token
    /// </summary>
    /// <returns>true if exists and it is enabled. otherwise false</returns>
    public Task<bool> ExistsUserInTokenAndIsEnabled();
    /// <summary>
    /// Check if exists a user
    /// using their usedId extracted from token
    /// It doesn't check if it is enabled
    /// </summary>
    /// <returns>true if exists. otherwise false</returns>
    public Task<bool> ExistsUserInToken();
    /// <summary>
    /// Get the userId in the token
    /// </summary>
    /// <returns>user id in the token if exists. otherwise null</returns>
    public string? GetUserIdFromToken();
    /// <summary>
    /// Generate a JWT token for the user
    /// </summary>
    /// <param name="userId">User id to assign token</param>
    /// <param name="userRole">User role to assign inside token</param>
    /// <param name="userName">User name to assign inside token</param>
    /// <returns>string if the user id exists or wasn't null. Otherwise null</returns>
    public string? GenerateToken(string userId, string userRole, string userName);
    /// <summary>
    /// Get the user id from the token
    /// </summary>
    /// <returns>string with the Id. null if something gone wrong</returns>
    public string? GetUserIdInToken();
    /// <summary>
    /// Get the user role from the token
    /// </summary>
    /// <returns>string with the Role. -1 if cannot be obtained</returns>
    public int GetUserRoleInToken();
    /// <summary>
    /// Check if exists a valid user role from the token
    /// </summary>
    /// <returns>true if its valid. otherwise false</returns>
    public bool ExistsUserRoleInToken();
    
    /// <summary>
    /// Check if the user found using userId extracted from token is specialist
    /// </summary>
    /// <param name="userId">User id to check</param>
    /// <returns>true if it is specialist. otherwise false</returns>
    public Task<bool> IsUserSpecialist();
    /// <summary>
    /// Get a enabled user by userId extracted from token and check
    /// if it is specialists
    /// </summary>
    /// <returns>User that match the filters. otherwise false</returns>
    public Task<User?> GetUserEnabledAndSpecialistFromToken();
    /// <summary>
    /// Get a enabled user by userId extracted from token and check
    /// if it is client
    /// </summary>
    /// <returns>User that match the filters. otherwise false</returns>
    public Task<User?> GetUserEnabledAndClientFromToken();

}
