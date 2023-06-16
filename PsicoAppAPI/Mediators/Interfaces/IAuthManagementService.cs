namespace PsicoAppAPI.Mediators.Interfaces;

public interface IAuthManagementService
{
    /// <summary>
    /// Check if exists a user and if it is enabled 
    /// using their usedId extracted from token
    /// </summary>
    /// <returns>true if exists and it is enabled. otherwise false</returns>
    public Task<bool> ExistsUserAndIsEnabled();
    /// <summary>
    /// Check if exists a user
    /// using their usedId extracted from token
    /// It doesn't check if it is enabled
    /// </summary>
    /// <returns>true if exists. otherwise false</returns>
    public Task<bool> ExistsUser();
}
