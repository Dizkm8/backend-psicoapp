namespace PsicoAppAPI.Mediators.Interfaces;

public interface IClientManagementService
{
    /// <summary>
    /// Check if a availability slot is available for the specialist with userId provided
    /// </summary>
    /// <param name="specialistUserId">user id of the specialist</param>
    /// <param name="availability">availability to check</param>
    /// <returns>True if it is available. false otherwise</returns>
    public Task<bool> IsSpecialistAvailable(string specialistUserId, DateTime availability);
}
