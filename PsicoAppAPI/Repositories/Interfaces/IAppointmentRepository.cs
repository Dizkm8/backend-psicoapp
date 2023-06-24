using System.Collections.Generic;
using System.Threading.Tasks;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Get all appointments of the database
        /// </summary>
        /// <returns>List with the Appointment</returns>
        Task<List<Appointment>?> GetAllAppointments();
        /// <summary>
        /// Get the appointments of a user based on their userId
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List with the Appointment</returns>
        Task<List<Appointment>?> GetAppointmentsByUser(string userId);
        /// <summary>
        /// Get the appointments of a user based on their userId
        /// descending orderer by bookedDate attribute of appointment
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List with the Appointment</returns>
        Task<List<Appointment>?> GetAppointmentsByUserOrderDesc(string userId);
        /// <summary>
        /// Add a new appointment to the database
        /// </summary>
        /// <param name="appointment">Appointment to add</param>
        /// <returns>true if could be added. otherwise false</returns>
        Task<bool> AddAppointmentAndSaveChanges(Appointment appointment);
        /// <summary>
        /// Cancel an appointment by their Id
        /// Also update the availability of their appointment to IsAvaialble = true
        /// </summary>
        /// <param name="appointmentId">Id of the appointment</param>
        /// <returns>true if could be deleted. otherwise false</returns>
        Task<bool> CancelAppointmentAndSaveChanges(int appointmentId);
    }
}
