namespace PsicoAppAPI.Repositories.Interfaces;

public interface IGptRulesRepository
{
    /// <summary>
    /// Get the rules from database
    /// </summary>
    /// <returns>string with rules, null if does not exists</returns>
    public Task<string?> GetRules();
}