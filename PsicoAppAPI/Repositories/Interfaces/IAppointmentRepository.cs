using System.Collections.Generic;
using System.Threading.Tasks;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointmentsByUser(int userId);
        /// <summary>
        /// Add a new appointment to the database
        /// </summary>
        /// <param name="appointment">Appointment to add</param>
        /// <returns>true if could be added. otherwise false</returns>
        Task<bool> AddAppointmentAndSaveChanges(Appointment appointment);
    }
}
