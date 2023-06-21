using PsicoAppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointmentsByUser(int userId);
        /// <summary>
        /// Add an appointment to the database
        /// When an appointment is added, the status is set to "Booked"
        /// </summary>
        /// <param name="RequestingUserId">userId of client</param>
        /// <param name="RequestedUserId">userId of specialist</param>
        /// <param name="BookedDate">date to book the appointment</param>
        /// <returns>true if could be added. otherwise false</returns>
        Task<bool> AddAppointment(string RequestingUserId, string RequestedUserId, DateTime BookedDate);
    }
}
