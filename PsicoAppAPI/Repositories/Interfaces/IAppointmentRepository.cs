using System.Collections.Generic;
using System.Threading.Tasks;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Get the appointments of a user based on their userId
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List with the Appointment</returns>
        Task<List<Appointment>> GetAppointmentsByUser(string userId);
        /// <summary>
        /// Add a new appointment to the database
        /// </summary>
        /// <param name="appointment">Appointment to add</param>
        /// <returns>true if could be added. otherwise false</returns>
        Task<bool> AddAppointmentAndSaveChanges(Appointment appointment);
    }
}
