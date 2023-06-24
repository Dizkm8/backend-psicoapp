using PsicoAppAPI.DTOs.User;

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
    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is admin
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsUserAdmin();
    /// <summary>
    /// Asynchronously check if a email is available to use
    /// </summary>
    /// <param name="specialistDto">User shape Dto to email validation</param>
    /// <returns>True if exists, otherwise false</returns>
    public Task<bool> CheckEmailAvailability(RegisterSpecialistDto specialistDto);
    /// <summary>
    /// Asynchronously check if a user exists by id
    /// </summary>
    /// <param name="specialistDto">User shape Dto to Id validation</param>
    /// <returns>True if exists, otherwise false</returns>
    public Task<bool> CheckUserIdAvailability(RegisterSpecialistDto specialistDto);
    /// <summary>
    /// Async add a new specialist in the system
    /// </summary>
    /// <param name="specialistDto">Specialist to add</param>
    /// <returns>true if user could be added. otherwise false</returns>
    public Task<bool> AddSpecialist(RegisterSpecialistDto specialistDto);
    /// <summary>
    /// Check if the speciality Id from RegisterSpecialistDto match with any speciality in the system
    /// </summary>
    /// <param name="specialistDto">RegisterSpecialistDto to check</param>
    /// <returns>true if exists. otherwise false</returns>
    public Task<bool> ExistsSpeciality(RegisterSpecialistDto specialistDto);
}