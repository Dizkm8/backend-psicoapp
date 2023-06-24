using PsicoAppAPI.DTOs;
using PsicoAppAPI.DTOs.Appointment;

namespace PsicoAppAPI.Mediators.Interfaces;

public interface IAppointmentManagementService
{
    /// <summary>
    /// Get the appointments of a user based on the userId from token
    /// </summary>
    /// <returns>IEnumerable with the Appointment as Dto. If something went wrong return null</returns>
    public Task<IEnumerable<AppointmentDto>?> GetAppointmentsByUser();
    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is client
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsUserClient();
    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is client or admin
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsAdminOrClient();
}