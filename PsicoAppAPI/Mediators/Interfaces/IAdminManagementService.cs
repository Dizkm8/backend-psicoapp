namespace PsicoAppAPI.Mediators.Interfaces;

public interface IAdminManagementService
{
    /// <summary>
    /// Get the current rules to moderate posts
    /// </summary>
    /// <returns>string with rules, null if do not exists</returns>
    public Task<string?> GetModerationRules();
    /// <summary>
    /// Set new rules to moderate posts
    /// </summary>
    /// <param name="newRules">New rules to set</param>
    /// <returns>true if rules could be updated. otherwise false</returns>
    public Task<bool> SetModerationRules(string newRules);
}