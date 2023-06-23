using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.Appointment;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IAppointmentManagementService
{
    /// <summary>
    /// Get the appointments of a user based on their userId
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <returns>IEnumerable with the Appointment as Dto</returns>
    public Task<IEnumerable<AppointmentDto>> GetAppointmentsByUser(string userId);
    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is client
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsUserClient();
}