using System.Collections.Generic;
using System.Threading.Tasks;
using PsicoAppAPI.Models;

namespace PsicoAppAPI.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointmentsByUser(int userId);
    }
}
