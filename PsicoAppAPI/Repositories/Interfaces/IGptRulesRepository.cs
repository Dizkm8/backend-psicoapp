using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces;

public interface IGptRulesRepository
{
    /// <summary>
    /// Get the rules from database
    /// </summary>
    /// <returns>string with rules, null if does not exists</returns>
    public Task<string?> GetRules();
    /// <summary>
    /// Update the rules from database
    /// </summary>
    /// <param name="rules">New rules to update</param>
    /// <returns>true if could be updated. otherwise false</returns>
    public Task<bool> SetRulesAndSaveChanges(GptRules rules);
}