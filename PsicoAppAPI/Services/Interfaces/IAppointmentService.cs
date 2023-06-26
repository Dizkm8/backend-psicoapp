using PsicoAppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Get the appointments of an user based on their userId
        /// descending orderer by BookedDate attribute of Appointment
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List with appointments</returns>
        Task<List<Appointment>> GetAppointmentsByClient(string userId);

        /// <summary>
        /// Get the appointments of an specialist based on their userId
        /// descending orderer by BookedDate attribute of Appointment
        /// </summary>
        /// <param name="userId">Id of the specialist</param>
        /// <returns>List with appointments</returns>
        Task<List<Appointment>> GetAppointmentsBySpecialist(string userId);

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
        /// <summary>
        /// Get the quantity of canceled appointments in the system
        /// </summary>
        /// <returns>Canceled amount. null if cannot be obtained</returns>
        Task<int?> GetCanceledAppointmentsQuantity();
        
        /// <summary>
        /// Get the quantity of booked appointments in the system
        /// </summary>
        /// <returns>Booked amount. null if cannot be obtained</returns>
        Task<int?> GetBookedAppointmentsQuantity();
        /// <summary>
        /// Get the quantity of done appointments in the system
        /// </summary>
        /// <returns>Done amount. null if cannot be obtained</returns>
        Task<int?> GetDoneAppointmentsQuantity();
    }
}