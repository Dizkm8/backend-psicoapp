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
    /// Get the appointments of a specialist based on the userId from token
    /// </summary>
    /// <returns>IEnumerable with the Appointment as Dto. If something went wrong return null</returns>
    public Task<IEnumerable<AppointmentDto>?> GetAppointmentsBySpecialist();
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

    /// <summary>
    /// Cancel an appointment
    /// It means the appointment is deleted and the avaialbility of specialist are restored to isAvailable = true
    /// </summary>
    /// <param name="appointmentId">Id of the appointment</param>
    /// <returns>null if cancel is not on range of 24 hours. true if could be canceled. otherwise false</returns>
    public Task<bool?> CancelAppointment(int appointmentId);

    /// <summary>
    /// Check using the token if the userId match with an enabled user and if it is specialist
    /// </summary>
    /// <returns>true if match with the filters. otherwise false</returns>
    public Task<bool> IsSpecialist();
}