using PsicoAppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Get the appointments of an user based on their userId
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List with appointments</returns>
        Task<List<Appointment>> GetAppointmentsByUser(string userId);
        /// <summary>
        /// Add an appointment to the database
        /// When an appointment is added, the status is set to "Booked"
        /// </summary>
        /// <param name="requestingUserId">userId of client</param>
        /// <param name="requestedUserId">userId of specialist</param>
        /// <param name="bookedDate">date to book the appointment</param>
        /// <returns>true if could be added. otherwise false</returns>
        Task<bool> AddAppointment(string requestingUserId, string requestedUserId, DateTime bookedDate);
        /// <summary>
        /// Cancel the appointment by their ID
        /// </summary>
        /// <param name="appointmentId">Id of the appointment</param>
        /// <returns>true if could be canceled. otherwise false</returns>
        Task<bool> CancelAppointment(int appointmentId);
        /// <summary>
        /// Get all the appointments of the system
        /// </summary>
        /// <returns>List with appointments</returns>
        Task<List<Appointment>> GetAllAppointments();
    }
}
